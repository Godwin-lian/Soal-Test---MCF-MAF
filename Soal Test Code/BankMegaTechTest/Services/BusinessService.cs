using System;
using BankMegaTechTest.Data;
using BankMegaTechTest.Models.DTO;
using BankMegaTechTest.Models.Entities;
using BankMegaTechTest.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace BankMegaTechTest.Services
{
	public class BusinessService : IBusinessService
	{
        private readonly ApplicationDbContext _context;

        public BusinessService(ApplicationDbContext context)
		{
            _context = context;
        }

        public async Task<List<MsStorageLocation>> GetAllLocation()
        {
            try
            {
                var locations = await _context.MsStorageLocations.ToListAsync();
                return locations;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve locations: {ex.Message}");
            }
            
        }

        public async Task<TrBpkb> GetBpkbByUserIdAsync(long userId)
        {
            try
            {
                var bpkb = await _context.TrBpkbs
                    .Where(b => b.UserId == userId)
                    .FirstOrDefaultAsync();

                if (bpkb != null)
                {
                    return bpkb;
                }
                else
                {
                    throw new Exception($"Failed to retrieve BPKB record");
                }
            }
            catch (Exception ex)
            {
                return new TrBpkb
                {
                    AgreementNumber = string.Empty,
                    BpkbNo = string.Empty,
                    BranchId = string.Empty,
                    BpkbDate = DateTime.MinValue,
                    FakturNo = string.Empty,
                    FakturDate = DateTime.MinValue,
                    LocationId = string.Empty,
                    PoliceNo = string.Empty,
                    BpkbDateIn = DateTime.MinValue,
                    UserId = userId 
                };
            }
        }


        public async Task<InsertBpkbResponseDTO> InsertBpkb(InsertBpkbRequestDTO requestDto)
        {
            try
            {
                var locationExists = await _context.MsStorageLocations
                    .AnyAsync(loc => loc.LocationId == requestDto.LocationId);

                if (!locationExists)
                {
                    throw new Exception($"LocationId does not exist.");

                }

                var existingBpkb = await _context.TrBpkbs
                    .FirstOrDefaultAsync(b => b.AgreementNumber == requestDto.AgreementNumber);

                if (existingBpkb != null)
                {
                    throw new Exception($"BPKB record is already exist.");
                }

                var newBpkb = new TrBpkb
                {
                    AgreementNumber = requestDto.AgreementNumber,
                    BpkbNo = requestDto.BpkbNo,
                    BranchId = requestDto.BranchId,
                    BpkbDate = requestDto.BpkbDate,
                    FakturNo = requestDto.FakturNo,
                    FakturDate = requestDto.FakturDate,
                    LocationId = requestDto.LocationId,
                    PoliceNo = requestDto.PoliceNo,
                    BpkbDateIn = requestDto.BpkbDateIn,
                    UserId = requestDto.userId
                };

                _context.TrBpkbs.Add(newBpkb);
                await _context.SaveChangesAsync();

                return new InsertBpkbResponseDTO
                {
                    Success = true,
                    Message = "BPKB inserted successfully."
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert BPKB: {ex.Message}");
            }
        }

        public async Task<InsertBpkbResponseDTO> UpdateBpkb(InsertBpkbRequestDTO requestDto)
        {
            try
            {
                // Check if the BPKB record exists
                var existingBpkb = await _context.TrBpkbs
                    .FirstOrDefaultAsync(b => b.AgreementNumber == requestDto.AgreementNumber);

                if (existingBpkb == null)
                {
                    throw new Exception($"BPKB record does not exist.");
                }

                // Check if the location exists
                var locationExists = await _context.MsStorageLocations
                    .AnyAsync(loc => loc.LocationId == requestDto.LocationId);

                if (!locationExists)
                {
                    throw new Exception($"LocationId does not exist.");
                }

                existingBpkb.AgreementNumber = requestDto.AgreementNumber;
                existingBpkb.BpkbNo = requestDto.BpkbNo;
                existingBpkb.BranchId = requestDto.BranchId;
                existingBpkb.BpkbDate = requestDto.BpkbDate;
                existingBpkb.FakturNo = requestDto.FakturNo;
                existingBpkb.FakturDate = requestDto.FakturDate;
                existingBpkb.LocationId = requestDto.LocationId;
                existingBpkb.PoliceNo = requestDto.PoliceNo;
                existingBpkb.BpkbDateIn = requestDto.BpkbDateIn;
                existingBpkb.UserId = requestDto.userId;


                _context.TrBpkbs.Update(existingBpkb);
                await _context.SaveChangesAsync();

                return new InsertBpkbResponseDTO
                {
                    Success = true,
                    Message = "BPKB updated successfully."
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update BPKB: {ex.Message}");
            }
        }

    }
}

