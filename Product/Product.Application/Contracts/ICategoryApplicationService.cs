﻿namespace Product.Application.Contracts
{
    using System.Collections.Generic;
    using Product.Application.Dto;

    public interface ICategoryApplicationService
    {
        CategoryOutputDto Get(int id);
        List<CategoryOutputDto> GetAll();
        void Create(CategoryCreateDto model);
    }
}
