using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.Services;
using Excellerent.SharedModules.DTO;
using LinqKit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Excellerent.ResourceManagement.UnitTests
{
    public class EmployeeServiceTest
    {
        private readonly EmployeeService _EmpService;
        private readonly Mock<IEmployeeRepository> EmpRepository = new Mock<IEmployeeRepository>();

        public EmployeeServiceTest()
        {
            _EmpService = new EmployeeService(EmpRepository.Object);
        }

        [Fact]
        public async Task GetAllEmployees()
        {
            Guid EmpId = Guid.NewGuid();
            var EmployeeEntity = new List<Employee>() {
            new Employee()
            {
                Guid = EmpId,
                FirstName = "abel",
                FatherName = "abel",
                GrandFatherName = "abel",
                DateofBirth = DateTime.Now,
                Nationality = new List<Nationality>() {

                    new Nationality() {
                        Guid = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Ethiopian",
                        CreatedbyUserGuid = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    }
                },
                IsActive = true,
                EmployeeOrganization = new EmployeeOrganization() {
                    Guid = Guid.NewGuid(),
                    Country = Guid.NewGuid().ToString(),
                    IsActive = true,
                    DutyBranch = Guid.NewGuid().ToString(),
                    Department = "HR",
                    CompaynEmail = "d@mail.com",
                    CreatedbyUserGuid = Guid.NewGuid(),
                    EmploymentType = "FullTime Permanent",
                    JobTitle = "manager",
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    EmployeeId = Guid.NewGuid(),
                    JoiningDate = DateTime.Now,
                    PhoneNumber = "251784596",
                    ReportingManager = "",
                    Status = "Active",
                    TerminationDate = DateTime.Now
                },
                CreatedbyUserGuid = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                EmergencyContact = new List<EmergencyContactsModel>() {
                new EmergencyContactsModel() {
                        Guid = Guid.NewGuid(),
                        CreatedbyUserGuid = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        FatherName = "abel",
                        FirstName = "abel",
                        IsActive = true,
                        IsDeleted = false,
                        Relationship = "bro",
                         houseNumber ="123123",
                          GrandFatherName="abel",
                           postalCode = "12313",
                            woreda = "12",
                              subCityZone = "Bole",
                               city = "AA",
                                Country = "Et",
                                 email = "nwwqaa@gmail.com",
                                  email2 = "nwqa@gmail.com",
                                   email3 ="retree@gmail.com",
                                    PhoneNumber = "+251987654",
                                     phoneNumber2 = "+251467892",
                                      phoneNumber3 ="+251987654",
                                       stateRegionProvice ="AA"

                }},
                Gender = "male",
                IsDeleted = false,
                MobilePhone = "25145126398",
                PersonalEmail = "asdasd@mail.com",
                PersonalEmail2 = "wasdasd@mail.com",
                PersonalEmail3 = "qasdasd@mail.com",
                Phone1 = "251879652",
                Phone2 = "251687541",
                Photo = "",
                EmployeeAddress = new List<PersonalAddress>() {

                            new PersonalAddress(){
                             Guid = Guid.NewGuid(),
                              City = "Addis",
                               Country="Ethio",
                                CreatedbyUserGuid = Guid.NewGuid(),
                                 CreatedDate = DateTime.Now,
                                  HouseNumber = "1452545",
                                   IsActive = true,
                                    IsDeleted = false,
                                     PhoneNumber = "25147856324",
                                      PostalCode = "12545",
                                       StateRegionProvice = "Addis",
                                        SubCityZone = "Bole",
                                         Woreda = "12"
                            }
                        },
                 FamilyDetails = new List<FamilyDetails>()
                 {
                     new FamilyDetails(){
                      Guid = Guid.NewGuid(),
                       CreatedbyUserGuid = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                          DoB = DateTime.Now,
                           EmployeeId = Guid.NewGuid(),
                            RelationshipId = Guid.NewGuid(),
                              FullName = "abel",
                               Gender = "male",
                                IsActive = true,
                                 IsDeleted = false,

                                   Remark = "ok",
                                     Relationship = new Relationship()


                     }
                 }

                 }
            }; 



            EmpRepository.Setup(x => x.GetEmployeesAsync()).ReturnsAsync(EmployeeEntity);
            //Act
            var retrivedEmp = await _EmpService.GetAllEmployeesAsync();
            //Assert
            Assert.Equal(EmployeeEntity, retrivedEmp);



        }

        [Fact]
        public async Task AddNewEmployeeEntry() 
        {
            Guid EmpId = Guid.NewGuid();
            var EmployeeEntity = new Employee() { 
           
                Guid = EmpId,
                FirstName = "abel",
                FatherName = "abel",
                GrandFatherName = "abel",
                DateofBirth = DateTime.Now,
                Nationality = new List<Nationality>() {

                    new Nationality() {
                        Guid = EmpId,
                        IsActive = true,
                        Name = "Ethiopian",
                        CreatedbyUserGuid = EmpId,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    }
                },
                IsActive = true,
                EmployeeOrganization = new EmployeeOrganization() {
                    Guid = EmpId,
                    Country = EmpId.ToString(),
                    IsActive = true,
                    DutyBranch = EmpId.ToString(),
                    Department = "HR",
                    CompaynEmail = "d@mail.com",
                    CreatedbyUserGuid = EmpId,
                    EmploymentType = "1",
                    JobTitle = "manager",
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    EmployeeId = EmpId,
                    JoiningDate = DateTime.Now,
                    PhoneNumber = "251784596",
                    ReportingManager = EmpId.ToString(),
                    Status = "1",
                    TerminationDate = DateTime.Now
                },
                CreatedbyUserGuid = EmpId,
                CreatedDate = DateTime.Now,
                EmergencyContact = new List<EmergencyContactsModel>() {
                    new EmergencyContactsModel()
                    {
                        Guid = Guid.NewGuid(),
                        CreatedbyUserGuid = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        FatherName = "abel",
                        FirstName = "abel",
                        IsActive = true,
                        IsDeleted = false,
                        Relationship = "bro",
                        houseNumber = "123123",
                        GrandFatherName = "abel",
                        postalCode = "12313",
                        woreda = "12",
                        subCityZone = "Bole",
                        city = "AA",
                        Country = "Et",
                        email = "nwwqaa@gmail.com",
                        email2 = "nwqa@gmail.com",
                        email3 = "retree@gmail.com",
                        PhoneNumber = "+251987654",
                        phoneNumber2 = "+251467892",
                        phoneNumber3 = "+251987654",
                        stateRegionProvice = "AA"

                    }},
                Gender = "male",
                IsDeleted = false,
                MobilePhone = "25145126398",
                PersonalEmail = "asdasd@mail.com",
                PersonalEmail2 = "wasdasd@mail.com",
                PersonalEmail3 = "qasdasd@mail.com",
                Phone1 = "251879652",
                Phone2 = "251687541",
                Photo = "",
                EmployeeAddress = new List<PersonalAddress>() {

                            new PersonalAddress(){
                             Guid = EmpId,
                              City = "Addis",
                               Country="Ethio",
                                CreatedbyUserGuid = EmpId,
                                 CreatedDate = DateTime.Now,
                                  HouseNumber = "1452545",
                                   IsActive = true,
                                    IsDeleted = false,
                                     PhoneNumber = "25147856324",
                                      PostalCode = "12545",
                                       StateRegionProvice = "Addis",
                                        SubCityZone = "Bole",
                                         Woreda = "12"
                            }
                        },
                 FamilyDetails = new List<FamilyDetails>()
                 {
                     new FamilyDetails(){
                      Guid = EmpId,
                       CreatedbyUserGuid = EmpId,
                        CreatedDate = DateTime.Now,
                          DoB = DateTime.Now,
                           EmployeeId = EmpId,
                            RelationshipId = EmpId,
                              FullName = "abel",
                               Gender = "male",
                                IsActive = true,
                                 IsDeleted = false,
                                   Remark = "ok",
                                     Relationship = new Relationship()


                     }
                 }

                 
            };


             EmpRepository.Setup(repo => repo.AddAsync(EmployeeEntity)).ReturnsAsync((EmployeeEntity));

            var savedEmp = await _EmpService.AddNewEmployeeEntry(EmployeeEntity);
            //Assert
            Assert.Equal(EmployeeEntity, savedEmp);

        }

        //[Fact]
        //public async Task GetAllEmployees_WithNo_Searching_Criteria()
        //{
        //    Arrange
        //    Guid EmpId = Guid.NewGuid();
        //    CountryEntity country = new CountryEntity
        //    {
        //        Guid = Guid.NewGuid(),
        //        Name = "Ethiopia",
        //        Nationality = "Ethiopian",
        //        CreatedDate = DateTime.Now
        //    };

        //    var employee = new Employee()
        //    {
        //        Guid = EmpId,
        //        FirstName = "abel",
        //        FatherName = "abel",
        //        GrandFatherName = "abel",
        //        DateofBirth = DateTime.Now,
        //        Nationality = new List<Nationality>() {

        //            new Nationality() {
        //                Guid = Guid.NewGuid(),
        //                IsActive = true,
        //                Name = "Ethiopian",
        //                CreatedbyUserGuid = Guid.NewGuid(),
        //                CreatedDate = DateTime.Now,
        //                IsDeleted = false
        //            }
        //        },
        //        IsActive = true,
        //        EmployeeOrganization = new EmployeeOrganization()
        //        {
        //            Guid = Guid.NewGuid(),
        //            CountryId = country.Guid,
        //            IsActive = true,
        //            DutyBranch = Guid.NewGuid(),
        //            Department = "HR",
        //            CompaynEmail = "d@mail.com",
        //            CreatedbyUserGuid = Guid.NewGuid(),
        //            EmploymentType = "FullTime Permanent",
        //            JobTitle = "manager",
        //            IsDeleted = false,
        //            CreatedDate = DateTime.Now,
        //            EmployeeId = Guid.NewGuid(),
        //            JoiningDate = DateTime.Now,
        //            PhoneNumber = "251784596",
        //            ReportingManager = "Tariku",
        //            Status = "Active",
        //            TerminationDate = DateTime.Now
        //        },
        //        CreatedbyUserGuid = Guid.NewGuid(),
        //        CreatedDate = DateTime.Now,
        //        EmergencyContact = new List<EmergencyContact>() {
        //            new EmergencyContact() {
        //                Guid = Guid.NewGuid(),
        //                CreatedbyUserGuid = Guid.NewGuid(),
        //                CreatedDate = DateTime.Now,
        //                FatherName = "abel",
        //                FirstName = "abel",
        //                IsActive = true,
        //                IsDeleted = false,
        //                Relationship = "bro",
        //                Address = new List<EmergencyAddress>()
        //                {
        //                    new EmergencyAddress()
        //                    {
        //                        Guid = Guid.NewGuid(),
        //                        CreatedDate = DateTime.Now,
        //                        City = "Addis",
        //                        Country = "Ethiopia",
        //                        CreatedbyUserGuid = Guid.NewGuid(),
        //                        HouseNumber = "12344",
        //                        IsActive = true,
        //                        IsDeleted = false,
        //                        PhoneNumber = "2514587896",
        //                        PostalCode = "1234",
        //                        StateRegionProvice = "Addis",
        //                        SubCityZone = "Bole",
        //                        Woreda = "12"
        //                    }
        //                }

        //            }

        //        },
        //        Gender = "male",
        //        IsDeleted = false,
        //        MobilePhone = "25145126398",
        //        PersonalEmail = "asdasd@mail.com",
        //        PersonalEmail2 = "wasdasd@mail.com",
        //        PersonalEmail3 = "qasdasd@mail.com",
        //        Phone1 = "251879652",
        //        Phone2 = "251687541",
        //        Photo = "",
        //        EmployeeAddress = new List<PersonalAddress>() {

        //                    new PersonalAddress(){
        //                     Guid = Guid.NewGuid(),
        //                      City = "Addis",
        //                       Country="Ethio",
        //                        CreatedbyUserGuid = Guid.NewGuid(),
        //                         CreatedDate = DateTime.Now,
        //                          HouseNumber = "1452545",
        //                           IsActive = true,
        //                            IsDeleted = false,
        //                             PhoneNumber = "25147856324",
        //                              PostalCode = "12545",
        //                               StateRegionProvice = "Addis",
        //                                SubCityZone = "Bole",
        //                                 Woreda = "12"
        //                    }
        //                },
        //        FamilyDetails = new List<FamilyDetails>()
        //         {
        //             new FamilyDetails(){
        //              Guid = Guid.NewGuid(),
        //               CreatedbyUserGuid = Guid.NewGuid(),
        //                CreatedDate = DateTime.Now,
        //                  DoB = DateTime.Now,
        //                   EmployeeId = Guid.NewGuid(),
        //                    RelationshipId = Guid.NewGuid(),
        //                      FullName = "abel",
        //                       Gender = "male",
        //                        IsActive = true,
        //                         IsDeleted = false,

        //                           Remark = "ok",
        //                             Relationship = new Relationship()


        //             }
        //         }

        //    };
        //    Excellerent.SharedModules.DTO.PredicatedResponseDTO objEmployeeViewModelList = new PredicatedResponseDTO()
        //    {
        //        new EmployeeViewModel()
        //        {
        //            EmployeeGUid = EmpId,
        //            FullName = employee.FirstName + ' ' + employee.FatherName  + ' '  + employee.GrandFatherName,
        //            JobTitle = employee.EmployeeOrganization.JobTitle,
        //            Location = country.Name,
        //            Status = "Active"
        //        }
        //    };
        //    string searchKey = null;
        //    var predicate = PredicateBuilder.True<Employee>();
        //    if (searchKey != null)
        //    {
        //        predicate = predicate.And(p => p.FirstName.Contains(searchKey)).Or(p => p.FatherName.Contains(searchKey)).Or(p => p.GrandFatherName.Contains(searchKey));
        //    }
        //    else
        //    {
        //        predicate = null;
        //    }

        //    EmpRepository.Setup(x => x.GetAllEmployeesDashboardAsync(predicate, 0, 5)).ReturnsAsync(objEmployeeViewModelList);
        //    Act
        //    var retrivedEmpList = await _EmpService.GetAllEmployeesDashboardAsync(searchKey, 0, 5);
        //    Assert
        //    Assert.Equal(objEmployeeViewModelList, retrivedEmpList);
        //}

        //[Fact]
        //public async Task GetAllEmployees_With_Searching_Criteria()
        //{
        //    Arrange
        //    Guid EmpId = Guid.NewGuid();
        //    CountryEntity country = new CountryEntity
        //    {
        //        Guid = Guid.NewGuid(),
        //        Name = "Ethiopia",
        //        Nationality = "Ethiopian",
        //        CreatedDate = DateTime.Now
        //    };
        //    var EmployeeEntity = new List<Employee>() {
        //    new Employee()
        //    {
        //        Guid = EmpId,
        //        FirstName = "abel",
        //        FatherName = "abel",
        //        GrandFatherName = "abel",
        //        DateofBirth = DateTime.Now,
        //        Nationality = new List<Nationality>() {

        //            new Nationality() {
        //                Guid = Guid.NewGuid(),
        //                IsActive = true,
        //                Name = "Ethiopian",
        //                CreatedbyUserGuid = Guid.NewGuid(),
        //                CreatedDate = DateTime.Now,
        //                IsDeleted = false
        //            }
        //        },
        //        IsActive = true,
        //        EmployeeOrganization = new EmployeeOrganization() {
        //            Guid = Guid.NewGuid(),
        //            CountryId = country.Guid,
        //            IsActive = true,
        //            DutyBranch = Guid.NewGuid(),
        //            Department = "HR",
        //            CompaynEmail = "d@mail.com",
        //            CreatedbyUserGuid = Guid.NewGuid(),
        //            EmploymentType = "FullTime Permanent",
        //            JobTitle = "manager",
        //            IsDeleted = false,
        //            CreatedDate = DateTime.Now,
        //            EmployeeId = Guid.NewGuid(),
        //            JoiningDate = DateTime.Now,
        //            PhoneNumber = "251784596",
        //            ReportingManager = "Tariku",
        //            Status = "Active",
        //            TerminationDate = DateTime.Now
        //        },
        //        CreatedbyUserGuid = Guid.NewGuid(),
        //        CreatedDate = DateTime.Now,
        //        EmergencyContact = new List<EmergencyContact>() {
        //            new EmergencyContact() {
        //                Guid = Guid.NewGuid(),
        //                CreatedbyUserGuid = Guid.NewGuid(),
        //                CreatedDate = DateTime.Now,
        //                FatherName = "abel",
        //                FirstName = "abel",
        //                IsActive = true,
        //                IsDeleted = false,
        //                Relationship = "bro",
        //                Address = new List<EmergencyAddress>()
        //                {
        //                    new EmergencyAddress()
        //                    {
        //                        Guid = Guid.NewGuid(),
        //                        CreatedDate = DateTime.Now,
        //                        City = "Addis",
        //                        Country = "Ethiopia",
        //                        CreatedbyUserGuid = Guid.NewGuid(),
        //                        HouseNumber = "12344",
        //                        IsActive = true,
        //                        IsDeleted = false,
        //                        PhoneNumber = "2514587896",
        //                        PostalCode = "1234",
        //                        StateRegionProvice = "Addis",
        //                        SubCityZone = "Bole",
        //                        Woreda = "12"
        //                    }
        //                }

        //            }

        //        },
        //        Gender = "male",
        //        IsDeleted = false,
        //        MobilePhone = "25145126398",
        //        PersonalEmail = "asdasd@mail.com",
        //        PersonalEmail2 = "wasdasd@mail.com",
        //        PersonalEmail3 = "qasdasd@mail.com",
        //        Phone1 = "251879652",
        //        Phone2 = "251687541",
        //        Photo = "",
        //        EmployeeAddress = new List<PersonalAddress>() {

        //                    new PersonalAddress(){
        //                     Guid = Guid.NewGuid(),
        //                      City = "Addis",
        //                       Country="Ethio",
        //                        CreatedbyUserGuid = Guid.NewGuid(),
        //                         CreatedDate = DateTime.Now,
        //                          HouseNumber = "1452545",
        //                           IsActive = true,
        //                            IsDeleted = false,
        //                             PhoneNumber = "25147856324",
        //                              PostalCode = "12545",
        //                               StateRegionProvice = "Addis",
        //                                SubCityZone = "Bole",
        //                                 Woreda = "12"
        //                    }
        //                },
        //         FamilyDetails = new List<FamilyDetails>()
        //         {
        //             new FamilyDetails(){
        //              Guid = Guid.NewGuid(),
        //               CreatedbyUserGuid = Guid.NewGuid(),
        //                CreatedDate = DateTime.Now,
        //                  DoB = DateTime.Now,
        //                   EmployeeId = Guid.NewGuid(),
        //                    RelationshipId = Guid.NewGuid(),
        //                      FullName = "abel",
        //                       Gender = "male",
        //                        IsActive = true,
        //                         IsDeleted = false,

        //                           Remark = "ok",
        //                             Relationship = new Relationship()


        //             }
        //         }

        //    }};
        //    List<EmployeeViewModel> employeeViewModelList = new List<EmployeeViewModel>()
        //    {
        //        new EmployeeViewModel()
        //        {
        //            EmployeeGUid = EmpId,
        //            FullName = EmployeeEntity[0].FirstName + ' ' + EmployeeEntity[0].FatherName  + ' '  + EmployeeEntity[0].GrandFatherName,
        //            JobTitle = EmployeeEntity[0].EmployeeOrganization.JobTitle,
        //            Location = country.Name,
        //            Status = "Active"
        //        }
        //    };
        //    string searchKey = "ab";
        //    var predicate = PredicateBuilder.True<Employee>();
        //    if (searchKey != "")
        //    {
        //        predicate = predicate.And(p => p.FirstName.Contains(searchKey)).Or(p => p.FatherName.Contains(searchKey)).Or(p => p.GrandFatherName.Contains(searchKey));
        //    }
        //    else
        //    {
        //        predicate = null;
        //    }

        //    EmpRepository.Setup(x => x.GetAllEmployeesDashboardAsync(predicate, 0, 5)).ReturnsAsync(employeeViewModelList);
        //    Act
        //    var retrivedEmpList = await _EmpService.GetAllEmployeesDashboardAsync(searchKey, 0, 5);
        //    Assert
        //    Assert.Equal(employeeViewModelList, retrivedEmpList);
        //}
    }
}
