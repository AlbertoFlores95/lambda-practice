using lamda_practice.Data;
using System;
using System.Globalization;
using System.Linq;

namespace lambda_practice
{
    class Program
    {

        static void Main(string[] args)
        {

            using (var ctx = new DatabaseContext())
            {

                int inputUser;

                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Seleccionar query a ejecutar");
                    Console.WriteLine("1. Listar todos los empleados cuyo departamento tenga una sede en Chihuahua");
                    Console.WriteLine("2. Listar todos los departamentos y el numero de empleados que pertenezcan a cada departamento.");
                    Console.WriteLine("3. Listar todos los empleados remotos. Estos son los empleados cuya ciudad no se encuentre entre las sedes de su departamento.");
                    Console.WriteLine("4. Listar todos los empleados cuyo aniversario de contratación sea el próximo mes.");
                    Console.WriteLine("5. Listar los 12 meses del año y el numero de empleados contratados por cada mes.");
                    Console.WriteLine();
                    Console.WriteLine("6. Salir");
                    inputUser = int.Parse(Console.ReadLine());


                    switch (inputUser)
                    {
                        case 1:
                            //1. Listar todos los empleados cuyo departamento tenga una sede en Chihuahua
                            var query1 = ctx.Employees

                     .Where(city => city.City.Name == "Chihuahua")
                     .Select(r => new { r.Id, r.FirstName, r.LastName });

                            foreach (var result1 in query1)
                            {
                                Console.WriteLine(" Id: {0} Name: {1} {2} ",
                                result1.Id, result1.FirstName, result1.LastName);
                            }


                            break;

                        case 2:
                            //2. Listar todos los departamentos y el numero de empleados que pertenezcan a cada departamento.
                            var query2 = ctx.Employees
                    .GroupBy(department => department.Department.Name)
                    .Select(r => new
                    {
                        employee = r.Key,
                        employee2 = r.Count()
                    });

                            foreach (var result2 in query2)
                            {
                                Console.WriteLine("Department: {0} - {1}", result2.employee, result2.employee2);
                            }
                            break;

                        case 3:
                            //3. Listar todos los empleados remotos. Estos son los empleados cuya ciudad no se encuentre entre las sedes de su departamento.

                            var query3 = ctx.Employees
                    .Where(employee => employee.Department.Cities.Any(city => city.Name == employee.City.Name));

                            foreach (var result3 in query3)
                            {
                                Console.WriteLine("ID: {0} Name: {1} {2}", result3.Id, result3.FirstName, result3.LastName);
                            }



                            break;

                        case 4:
                            //4. Listar todos los empleados cuyo aniversario de contratación sea el próximo mes.
                            var query4 = ctx.Employees
                                            .Where(employee => employee.HireDate.Month == System.DateTime.Now.Month + 1);

                            foreach (var result4 in query4)
                            {
                                Console.WriteLine("ID: {0} Name: {1} {2}", result4.Id, result4.FirstName, result4.LastName);
                            }
                            break;

                        case 5:
                            //5. Listar los 12 meses del año y el numero de empleados contratados por cada mes.
                            var query5 = ctx.Employees.GroupBy(e => e.HireDate.Month);

                            foreach (var x in query5)
                            {
                                Console.WriteLine("{0} - {1}", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.Key), x.Count());
                            }

                            break;
                    }

                } while (inputUser != 6);
                Console.Read();
            }
        }
    }
}