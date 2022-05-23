using BookStore.Data.Interface;
using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using BookStore.Model.Dto;

namespace BookStore.Core
{
    public class BookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> GetAll()
        {
            var model = await _unitOfWork.Book.GetAll();
            return new ResponseModel
            {
                ResponseData = model.Include(x => x.Category).Where(x => x.IsDeleted == false),
                ResponseCode = HttpStatusCode.OK,
                ResponseMessage = $"Found {model.Count()} Records"
            };
        }

        public async Task<ResponseModel> GetById(Guid Id)
        {
            var model = await _unitOfWork.Book.GetByGuidId(Id);
            if (model == null)
            {
                return new ResponseModel
                {
                    ResponseData = null,
                    ResponseCode = HttpStatusCode.OK,
                    ResponseMessage = $"Found No Record With Id {Id}"
                };
            }
            return new ResponseModel
            {
                ResponseData = model,
                ResponseCode = HttpStatusCode.OK,
                ResponseMessage = $"Found Record With Id {Id}"
            };
        }

        public async Task<ResponseModel> GetAllFavoriteBooks()
        {
            var model = await _unitOfWork.Book.GetAll();
            var response = model.Include(x => x.Category).Where(x => x.IsDeleted == false && x.IsFavorite == true);
            return new ResponseModel
            {
                ResponseData = response,
                ResponseCode = HttpStatusCode.OK,
                ResponseMessage = $"Found {response.Count()} Records"
            };
        }

        public async Task<ResponseModel> CreateOrEdit(CreateOrUpdateBookDto input)
        {
            if (input?.BookId == Guid.Empty || input?.BookId == null)
            {
                var response = await Create(input);
                return response;
            }
            else
            {
                var response = await Update(input);
                return response;
            }
        }

        public async Task<ResponseModel> LendBook(Guid Id)
        {
            var book = await _unitOfWork.Book.GetByGuidId(Id);
            if(book == null)
                return new ResponseModel
                {
                    ResponseData = null,
                    ResponseCode = HttpStatusCode.BadRequest,
                    ResponseMessage = $"Found No Record With Id {Id}"
                };
            if(book.Status == "Borrowed")
                return new ResponseModel
                {
                    ResponseData = null,
                    ResponseCode = HttpStatusCode.BadRequest,
                    ResponseMessage = $"Book With Id {Id} Is Currently Not Available"
                };

            book.Status = "Borrowed";
            book.BorrowedCount += 1;
            book.ModifiedDate = DateTime.Now;

            _unitOfWork.Book.Update(book);
            await _unitOfWork.Complete();

            return new ResponseModel
            {
                ResponseData = book,
                ResponseCode = HttpStatusCode.OK,
                ResponseMessage = $"Book Borrowed"
            };
        }



        public async Task<ResponseModel> Delete(Guid Id)
        {
            var category = await _unitOfWork.Category.GetByGuidId(Id);
            if (category == null)
            {
                return new ResponseModel
                {
                    ResponseData = null,
                    ResponseCode = HttpStatusCode.BadRequest,
                    ResponseMessage = $"Found No Record With Id {Id}"
                };
            }

            category.IsDeleted = true;

            _unitOfWork.Category.Update(category);
            await _unitOfWork.Complete();

            return new ResponseModel
            {
                ResponseCode = HttpStatusCode.OK,
                ResponseData = category,
                ResponseMessage = "Category Deleted Successfully"
            };
        }

        protected virtual async Task<ResponseModel> Create(CreateOrUpdateBookDto input)
        {
            var book = new Book();
            var existing = await _unitOfWork.Book.GetByName(input.BookName);

            if (existing != null)
                return new ResponseModel
                {
                    ResponseCode = HttpStatusCode.BadRequest,
                    ResponseData = existing,
                    ResponseMessage = "Book already exists"
                };
            var catogory = _unitOfWork.Category.GetByGuidId(input.CategoryId);
            if(catogory == null)
                return new ResponseModel
                {
                    ResponseCode = HttpStatusCode.BadRequest,
                    ResponseData = catogory,
                    ResponseMessage = $"No Book Category Found with {input.CategoryId}"
                };

            book.Id = Guid.NewGuid();
            book.CategoryId = input.CategoryId;
            book.Name = input.BookName;
            book.ModifiedDate = DateTime.Now;
            book.CreatedDate = DateTime.Now;
            book.IsDeleted = false;

            _unitOfWork.Book.Add(book);
            await _unitOfWork.Complete();

            return new ResponseModel
            {
                ResponseCode = HttpStatusCode.OK,
                ResponseData = book,
                ResponseMessage = "Request Created Successfully"
            };
        }


        protected virtual async Task<ResponseModel> Update(CreateOrUpdateBookDto input)
        {
            var book = await _unitOfWork.Book.GetByGuidId(input.BookId);
            if (book == null)
                return new ResponseModel
                {
                    ResponseCode = HttpStatusCode.BadRequest,
                    ResponseData = null,
                    ResponseMessage = "Book already exists"
                };
            var catogory = _unitOfWork.Category.GetByGuidId(input.CategoryId);
            if (catogory == null)
                return new ResponseModel
                {
                    ResponseCode = HttpStatusCode.BadRequest,
                    ResponseData = catogory,
                    ResponseMessage = $"No Book Category Found with {input.CategoryId}"
                };

            book.Name = input.BookName ?? book.Name;
            book.Status = input.Status ?? book.Status;

            if (book.BorrowedCount >= 5)
                book.IsFavorite = true;

            _unitOfWork.Book.Update(book);
            await _unitOfWork.Complete();

            return new ResponseModel
            {
                ResponseCode = HttpStatusCode.OK,
                ResponseData = book,
                ResponseMessage = "Request Updated Successfully"
            };
        }
    }
}
