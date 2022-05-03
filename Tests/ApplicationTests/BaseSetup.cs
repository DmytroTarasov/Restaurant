using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;

namespace Tests.ApplicationTests
{
    public class BaseSetup
    {
        protected Mock<IUnitOfWork> _uof;
        protected IMapper _mapper;

        [SetUp]
        public void Setup() {
            _uof = new Mock<IUnitOfWork>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingConfiguration());
            });
            _mapper = mappingConfig.CreateMapper();
        }
    }
}