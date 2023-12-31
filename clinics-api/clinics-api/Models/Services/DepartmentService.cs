﻿using clinics_api.Data;
using clinics_api.Models.DTOs;
using clinics_api.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace clinics_api.Models.Services
{
    public class DepartmentService : IDepartment
    {
        private readonly ClinicsDbContext _Db;
        public DepartmentService(ClinicsDbContext db)
        {
            _Db = db;
        }
        public async Task<bool> CreatDepartment(CreatDepartmentDto creatDepartmentDto)
        {
            if (creatDepartmentDto == null) { return false; }
            var department = (Department)creatDepartmentDto; 
            await _Db.Departments.AddAsync(department); 
            await _Db.SaveChangesAsync();
            return true;    
        }

        public async Task<bool> DeleteDepartment(int id)
        {
           var department= await _Db.Departments.FindAsync(id);

            if(department == null) { return false; }   
             _Db.Departments.Remove(department);
            await _Db.SaveChangesAsync();  
            return true;    
        }

        public async Task<List<DepartmentDto>> GetDepartments()
        {
            var departments= await _Db.Departments.Select(dep=> new DepartmentDto
            {
                DepartmentId=dep.DepartmentId,
                DepartmentName=dep.DepartmentName,
                IsDisabal=dep.IsDisabal,

            }).ToListAsync();

            return departments; 
        }
        public async Task<List<LocationDto>> GetLocations()
        {
            var locations = await _Db.Locations.Select(loc => new LocationDto
            {
                LocationId = loc.LocationId,
                LocationName = loc.LocationName,
                IsDisabal = loc.IsDisabal,

            }).ToListAsync();

            return locations;
        }
        public async Task<DepartmentDoctorsDto> GetDoctorDepartment(int id)
        {
            var depDoctors = await _Db.Departments.Select(dep=> new DepartmentDoctorsDto
            {
                DepartmentId = dep.DepartmentId,
                DepartmentName=dep.DepartmentName,  
                IsDisabal=dep.IsDisabal,
                Doctors=dep.Doctors.ToList(),

            }).FirstOrDefaultAsync(de=> de.DepartmentId==id);
            if(depDoctors == null) return null;
            return depDoctors;
        }

        

        public async Task<bool> UpdateDepartment(UpdateDepartmentDto updateDepartmentDto,int id)
        {
            if (updateDepartmentDto == null) return false;
            var dep =(Department) updateDepartmentDto;
            dep.DepartmentId = id;
            _Db.Entry(dep).State = EntityState.Modified;
            await _Db.SaveChangesAsync();
            return true;

        }
    }
}
