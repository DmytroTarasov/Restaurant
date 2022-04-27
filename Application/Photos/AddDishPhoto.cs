using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence;
using Persistence.Interfaces;

namespace Application.Photos
{
    public class AddDishPhoto  {
        public class Command : IRequest<Result<PhotoDTO<string>>> {
           public IFormFile File { get; set; }
           public Guid DishId { get; set; }
       }

       public class Handler : IRequestHandler<Command, Result<PhotoDTO<string>>>
        {
            private readonly IUnitOfWork _uof;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uof, 
                IPhotoAccessor photoAccessor,
                IMapper mapper)
            {
                _uof = uof;
                _photoAccessor = photoAccessor;
                _mapper = mapper;
            }

            public async Task<Result<PhotoDTO<string>>> Handle(Command request, CancellationToken cancellationToken)
            {
                var photoUploadResult = await _photoAccessor.AddPhoto(request.File);

                var photo = new Photo {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId
                };
                
                var dish = await _uof.DishRepository.Get(request.DishId);

                dish.Photo = photo;

                var result = await _uof.Complete();
                if (result) return Result<PhotoDTO<string>>.Success(_mapper.Map<Photo, PhotoDTO<string>>(photo));
                return Result<PhotoDTO<string>>.Failure("Problem with adding a photo to a dish");
            }
        }
    }
}