using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.ResourceManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.TestData.ResourceManagement
{
    public static class EmployeeTestData
    {
        private static readonly DateTime _joiningDate = new DateTime(2021, 8, 2);
        private static readonly DateTime _dateofBirth = new DateTime(2000, 1, 1);
        public static readonly List<Employee> _clientEmployees = new List<Employee>()
        {
            new Employee()
            {
                Guid = Guid.Parse("8A24C534-639E-41B4-C7C1-189C4FA2D855"),
                Organization="Excellerent1",
                FirstName = "Meba",
                FatherName = "Feysa",
                GrandFatherName = "Tesfaye",
                Gender="Female",
                DateofBirth=_dateofBirth,
                PersonalEmail="mfeyissa@gmail.com",
                MobilePhone="+25192795619",
                Nationality=new List<Nationality>()
                {
                    new Nationality()
                    {
                        Name="Ethiopian"
                    }
                },
                EmployeeOrganization= new EmployeeOrganization()
                {
                    Country="Ethiopia",
                    DutyBranch="Wengelawit A.A.",
                    CompaynEmail="mfeyissa@excellerentsolutions.com",
                    PhoneNumber="+25192795619",
                    JobTitle="Product Owner",
                    BusinessUnit="Software Development",
                    Department="Development Team",
                    EmploymentType="Full Time Permanent",
                    JoiningDate=_joiningDate,
                    Status="Active"

                }
            },
            new Employee()
            {
                Guid = Guid.Parse("7C56FB39-AA78-2DA5-7B75-BD3AB937341A"),
                Organization="Excellerent2",
                FirstName = "Husien",
                FatherName = "Seid",
                GrandFatherName = "Ahmed",
                Gender="Male",
                DateofBirth=_dateofBirth,
                PersonalEmail="hseid@gmail.com",
                MobilePhone="+251912891914",
                Nationality=new List<Nationality>()
                {
                    new Nationality()
                    {
                        Name="Ethiopian"
                    }
                },
                EmployeeOrganization= new EmployeeOrganization()
                {
                    Country="Ethiopia",
                    DutyBranch="Wengelawit A.A.",
                    CompaynEmail="hseid@excellerentsolutions.com",
                    PhoneNumber="+251912891914",
                    JobTitle="Product Owner",
                    BusinessUnit="Software Development",
                    Department="Development Team",
                    EmploymentType="Full Time Permanent",
                    JoiningDate=_joiningDate,
                    Status="Active"

                }
            }
        };
        public static readonly List<Employee> _managingEmployees = new List<Employee>()
        {
            new Employee()
            {
                Guid = Guid.Parse("662E0A2D-B4B1-46D3-11AC-4864188B2BBA"),
                FirstName = "Tariku",
                FatherName = "Worku",
                GrandFatherName = "Tesfaye",
                Gender="Male",
                DateofBirth=_dateofBirth,
                PersonalEmail="tworku@gmail.com",
                MobilePhone="+251930642374",
                Nationality=new List<Nationality>()
                {
                    new Nationality()
                    {
                        Name="Ethiopian"
                    }
                },
                EmployeeOrganization= new EmployeeOrganization()
                {
                    Country="Ethiopia",
                    DutyBranch="Wengelawit A.A.",
                    CompaynEmail="tworku@excellerentsolutions.com",
                    PhoneNumber="+251930642374",
                    JobTitle="Manager",
                    BusinessUnit="Software Development",
                    Department="Development Team",
                    EmploymentType="Full Time Permanent",
                    JoiningDate=_joiningDate,
                    Status="Active"

                }
            },
            new Employee()
            {
                Guid = Guid.Parse("E718383C-36BA-AD21-2CA6-87A55D6C9E85"),
                FirstName = "Nathnael",
                FatherName = "Getahun",
                GrandFatherName = "--",
                Gender="Male",
                DateofBirth=_dateofBirth,
                PersonalEmail="ngetahun@gmail.com",
                MobilePhone="+251911474539",
                Nationality=new List<Nationality>()
                {
                    new Nationality()
                    {
                        Name="Ethiopian"
                    }
                },
                EmployeeOrganization= new EmployeeOrganization()
                {
                    Country="Ethiopia",
                    DutyBranch="Wengelawit A.A.",
                    CompaynEmail="ngetahun@excellerentsolutions.com",
                    PhoneNumber="+251911474539",
                    JobTitle="Manager",
                    BusinessUnit="Software Development",
                    Department="Development Team",
                    EmploymentType="Full Time Permanent",
                    JoiningDate=_joiningDate,
                    Status="Active"

                }
            }
        };
        public static readonly List<Employee> _sampleEmployees = new List<Employee>()
        {
            new Employee()
            {
                Guid = Guid.Parse("079898D0-C73A-8816-3DAA-C89357FCB8D2"),
                FirstName = "Ashnafi",
                FatherName = "Feseha",
                GrandFatherName = "Tesfaye",
                Gender="Male",
                DateofBirth=_dateofBirth,
                PersonalEmail="afiseha@gmail.com",
                MobilePhone="+251983351881",
                Nationality=new List<Nationality>()
                {
                    new Nationality()
                    {
                        Name="Ethiopian"
                    }
                },
                EmployeeOrganization= new EmployeeOrganization()
                {
                    Country="Ethiopia",
                    DutyBranch="Wengelawit A.A.",
                    CompaynEmail="afiseha@excellerentsolutions.com",
                    PhoneNumber="+251983351881",
                    JobTitle="Developer",
                    BusinessUnit="Software Development",
                    Department="Development Team",
                    EmploymentType="Full Time Permanent",
                    JoiningDate=_joiningDate,
                    Status="Active"

                }
            },
            new Employee()
            {
                Guid = Guid.Parse("c0b74644-b81a-4c33-a6b0-672ba4bc8cb2"),
                FirstName = "Hana",
                FatherName = "Birhanu",
                GrandFatherName = "Tesfaye",
                Gender="Female",
                DateofBirth=_dateofBirth,
                PersonalEmail="hbirhanu@gmail.com",
                MobilePhone="+251918300720",
                Nationality=new List<Nationality>()
                {
                    new Nationality()
                    {
                        Name="Ethiopian"
                    }
                },
                EmployeeOrganization= new EmployeeOrganization()
                {
                    Country="Ethiopia",
                    DutyBranch="Wengelawit A.A.",
                    CompaynEmail="hbirhanu@excellerentsolutions.com",
                    PhoneNumber="+251918300720",
                    JobTitle="QA",
                    BusinessUnit="Software Development",
                    Department="Development Team",
                    EmploymentType="Full Time Permanent",
                    JoiningDate=_joiningDate,
                    Status="Active"

                }
            },
            new Employee()
            {
                Guid = Guid.Parse("0724B277-4373-ACD5-3EB4-5964A57471A1"),
                FirstName = "Joseph",
                FatherName = "Assefa",
                GrandFatherName = "Haile",
                Gender="Male",
                DateofBirth=_dateofBirth,
                PersonalEmail="jassefa@gmail.com",
                MobilePhone="+251945948911",
                Nationality=new List<Nationality>()
                {
                    new Nationality()
                    {
                        Name="Ethiopian"
                    }
                },
                EmployeeOrganization= new EmployeeOrganization()
                {
                    Country="Ethiopia",
                    DutyBranch="Wengelawit A.A.",
                    CompaynEmail="jassefa@excellerentsolutions.com",
                    PhoneNumber="251945948911",
                    JobTitle="Developer",
                    BusinessUnit="Software Development",
                    Department="Development Team",
                    ReportingManager=Guid.NewGuid().ToString(),
                    EmploymentType="Full Time Permanent",
                    JoiningDate=_joiningDate,
                    Status="Active"

                }
            },
            new Employee()
            {
                Guid = Guid.Parse("C38643DA-B684-532B-DBE1-37EA98D9B895"),
                FirstName = "Amanuel",
                FatherName = "Zewedu",
                GrandFatherName = "Haile",
                Gender="Male",
                DateofBirth=_dateofBirth,
                PersonalEmail="azewedu@gmail.com",
                MobilePhone="+251965044554",
                Nationality=new List<Nationality>()
                {
                    new Nationality()
                    {
                        Name="Ethiopian"
                    }
                },
                EmployeeOrganization= new EmployeeOrganization()
                {
                    Country="Ethiopia",
                    DutyBranch="Wengelawit A.A.",
                    CompaynEmail="azewedu@excellerentsolutions.com",
                    PhoneNumber="251965044554",
                    JobTitle="Developer",
                    BusinessUnit="Software Development",
                    Department="Development Team",
                    EmploymentType="Full Time Permanent",
                    JoiningDate=_joiningDate,
                    Status="Active"
                }
            },
            new Employee()
            {
                Guid = Guid.Parse("2D150D3A-E015-3628-3EA6-F3E097059A14"),
                FirstName = "Yasechalew",
                FatherName = "Erkyehun",
                GrandFatherName = "Haile",
                Gender="Male",
                DateofBirth=_dateofBirth,
                PersonalEmail="yerkyehun@gmail.com",
                MobilePhone="+2519123444554",
                Nationality=new List<Nationality>()
                {
                    new Nationality()
                    {
                        Name="Ethiopian"
                    }
                },
                EmployeeOrganization= new EmployeeOrganization()
                {
                    Country="Ethiopia",
                    DutyBranch="Wengelawit A.A.",
                    CompaynEmail="yerkyehun@excellerentsolutions.com",
                    PhoneNumber="2519123444554",
                    JobTitle="Developer",
                    BusinessUnit="Software Development",
                    Department="Development Team",
                    EmploymentType="Full Time Permanent",
                    JoiningDate=_joiningDate,
                    Status="Active"

                }
            }
        };

        public static async Task Clear(IEmployeeRepository repo)
        {
            IEnumerable<Employee> data = await repo.GetAllAsync();
            var reply = data.Select(x => repo.DeleteAsync(x));
        }

        public static async Task Add(IEmployeeRepository repo)
        {
            IEnumerable<Employee> employees = await repo.GetAllAsync();
            // Insert managers' test data
            for (int i = 0; i < _clientEmployees.Count; i++)
            {
                var employeeIn = employees.Where(x => x.FirstName.Equals(_clientEmployees[i].FirstName));
                if (employeeIn.Count() == 0)
                {
                    _clientEmployees[i].EmployeeOrganization.ReportingManager = _clientEmployees[i].Guid.ToString();
                    _clientEmployees[i] = await repo.AddAsync(_clientEmployees[i]);
                }
                else
                {
                    _clientEmployees[i] = employeeIn.FirstOrDefault();
                }
            }
            Guid reportingManagersId = Guid.NewGuid(); // Tariku's Guid
            // Insert managers' test data
            for (int i = 0; i < _managingEmployees.Count; i++)
            {
                var employeeIn = employees.Where(x => x.FirstName.Equals(_managingEmployees[i].FirstName));
                if (employeeIn.Count() == 0)
                {
                    _managingEmployees[i].EmployeeOrganization.ReportingManager = reportingManagersId.ToString();
                    _managingEmployees[i] = await repo.AddAsync(_managingEmployees[i]);
                }
                else
                {
                    _managingEmployees[i] = employeeIn.FirstOrDefault();
                }
            }
            // Insert employees' test data
            for (int i = 0; i < _sampleEmployees.Count; i++)
            {
                var employeeIn = employees.Where(x => x.FirstName.Equals(_sampleEmployees[i].FirstName));
                if (employeeIn.Count() == 0)
                {
                    _sampleEmployees[i].EmployeeOrganization.ReportingManager = reportingManagersId.ToString();
                    _sampleEmployees[i] = await repo.AddAsync(_sampleEmployees[i]);
                }
                else
                {
                    _sampleEmployees[i] = employeeIn.FirstOrDefault();
                }
            }
        }
    }
}
