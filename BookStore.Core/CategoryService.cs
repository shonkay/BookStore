using AutoMapper;
using BookStore.Data.Interface;
using BookStore.Model;
using BookStore.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core
{
    public class CategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel> GetAll()
        {
            var model = await _unitOfWork.Category.GetAll();
            return new ResponseModel
            {
                ResponseData = model.Where(x => x.IsDeleted == false),
                ResponseCode = HttpStatusCode.OK,
                ResponseMessage = $"Found {model.Count()} Records"
            };
        }

        public async Task<ResponseModel> GetById(Guid Id)
        {
            var model = await _unitOfWork.Category.GetByGuidId(Id);
            if(model == null)
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

        public async Task<ResponseModel> CreateOrEdit(CreateOrUpdateCategoryDto input)
        {
            if (input?.CategoryId == Guid.Empty || input?.CategoryId == null)
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

        protected virtual async Task<ResponseModel> Create(CreateOrUpdateCategoryDto input)
        {
            var category = new Category();
            var existing = await _unitOfWork.Category.GetByName(input.CategoryName);

            if (existing != null)
                return new ResponseModel
                {
                    ResponseCode = HttpStatusCode.BadRequest,
                    ResponseData = existing,
                    ResponseMessage = "Category already exists"
                };
            category.Id = Guid.NewGuid();
            category.Name = input.CategoryName;
            category.ModifiedDate = DateTime.Now;
            category.CreatedDate = DateTime.Now;
            category.IsDeleted = false;

             _unitOfWork.Category.Add(category);
             await _unitOfWork.Complete();

            return new ResponseModel
            {
                ResponseCode = HttpStatusCode.OK,
                ResponseData = category,
                ResponseMessage = "Category Created Successfully"
            };
        }


        protected virtual async Task<ResponseModel> Update(CreateOrUpdateCategoryDto input)
        {
            var category = await _unitOfWork.Category.GetByGuidId(input.CategoryId);
            if (category == null)
                return new ResponseModel
                {
                    ResponseCode = HttpStatusCode.BadRequest,
                    ResponseData = null,
                    ResponseMessage = "Category already exists"
                };

            category.Name = input.CategoryName ?? category.Name;

             _unitOfWork.Category.Update(category);
            await _unitOfWork.Complete();

            return new ResponseModel
            {
                ResponseCode = HttpStatusCode.OK,
                ResponseData = category,
                ResponseMessage = "Category Updated Successfully"
            };
        }
    }
}
