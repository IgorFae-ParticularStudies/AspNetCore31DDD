using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserDto> Get(Guid id)
        {
            // Não posso mais retornar diretamente uma entidade
            // Preciso armazenar seu retorno depois converte-la
            var entity =  await _repository.SelectAsync(id);
            // Aqui converto a entidade que veio do banco para o UserDto
            return _mapper.Map<UserDto>(entity); // ?? new UserDto(); Modificamos para retornar null
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var listEntity =  await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<UserDto>>(listEntity);
        }

        public async Task<UserDtoCreateResult> Post(UserDtoCreate user)
        {
            // Dto para Model
            var model = _mapper.Map<UserModel>(user);
            // Model Para Entity
            var entity = _mapper.Map<UserEntity>(model);
            // Persistência com a Entidade
            var result =  await _repository.InsertAsync(entity);
            //Entidade para Dto de Create
            return _mapper.Map<UserDtoCreateResult>(result);
        }

        public async Task<UserDtoUpdateResult> Put(UserDtoUpdate user)
        {
            // Dto para Model
            var model = _mapper.Map<UserModel>(user);
            // Model Para Entity
            var entity = _mapper.Map<UserEntity>(model);
            // Persistência com a Entidade
            var result =  await _repository.UpdateAsync(entity);
            //Entidade para Dto de Update
            return _mapper.Map<UserDtoUpdateResult>(result);
        }
    }
}
