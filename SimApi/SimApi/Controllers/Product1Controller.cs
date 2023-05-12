﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimApi.Data.Domain;
using SimApi.Data.Uow;
using SimApi.Schema;

namespace SimApi.Service.Controllers;

[Route("simapi/v1/[controller]")]
[ApiController]
[NonController]
public class Product1Controller : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    private IMapper mapper;
    public Product1Controller(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }


    [HttpGet]
    public List<ProductResponse> GetAll()
    {
        var list = unitOfWork.GetRepository<Product>().GetAll();
        var mapped = mapper.Map<List<ProductResponse>>(list);
        return mapped;
    }

    [HttpGet("{id}")]
    public ProductResponse GetById(int id)
    {
        var row = unitOfWork.GetRepository<Product>().GetById(id);
        var mapped = mapper.Map<ProductResponse>(row);
        return mapped;
    }

    [HttpPost]
    public void Post([FromBody] ProductRequest request)
    {
        var entity = mapper.Map<Product>(request);
        unitOfWork.GetRepository<Product>().Insert(entity);
        unitOfWork.CompleteWithTransaction();
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] ProductResponse request)
    {
        request.Id = id;
        var entity = mapper.Map<Product>(request);
        unitOfWork.GetRepository<Product>().Update(entity);
        unitOfWork.Complete();
    }


    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        unitOfWork.GetRepository<Product>().DeleteById(id);
        unitOfWork.Complete();
    }

}
