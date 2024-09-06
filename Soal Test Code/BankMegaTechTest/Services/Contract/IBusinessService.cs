using System;
using BankMegaTechTest.Models.DTO;
using BankMegaTechTest.Models.Entities;

namespace BankMegaTechTest.Services.Contract
{
    public interface IBusinessService
    {
        public Task<List<MsStorageLocation>> GetAllLocation();
        public Task<InsertBpkbResponseDTO> InsertBpkb(
            InsertBpkbRequestDTO requestDto
        );
        public Task<InsertBpkbResponseDTO> UpdateBpkb(
            InsertBpkbRequestDTO requestDto
        );
        public Task<TrBpkb> GetBpkbByUserIdAsync(long userId);
    }

}

