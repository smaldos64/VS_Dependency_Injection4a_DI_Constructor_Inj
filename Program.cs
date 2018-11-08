using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Her er der implementeret DI - Dependency Injection (Pattern) med constructor injection.
// Her injecter vi vores Dataklasse ind i constructeren i vores Business logik klasse. Det er 
// således meget let/lettere nu, at lave et Business logik objekt, der anvender funktionaliteten i 
// en anden klasse. Det eneste krav til Dataklasserne er igen, at de implementerer funktionaliteten
// specificeret i ICustomerDataAccess interfacet.
// Vores injector klasse (CustomerService) står for at injecte konstruktoren i vores Business logik 
// klasse. Vi kan også injecte direkte ind i vores Business logik klasse, som det også er vist i 
// eksemplet her. Men så går vi lidt uden om konceptet med at have en injector klasse. Men det er selvfølgelig lettere
// at gøre det på denne måde, som det også ses i eksemplet her, hvor det lettere at ændre Dataklasse, hvis man direkte laver 
// et objekt af vores business logik klasse. 
// Vil vi opnå det samme med en CustomerService klasse, er vi også nødt til at lave f.eks. constructor injection i
// denne klasse også. Dette er også vist herunder for klassen CustomerService_Constructor_Injection_Data og objekter 
// af denne klasse.

namespace Dependency_Injection4_DI_Constructor_Injection
{

    public interface ICustomerDataAccess
    {
        string GetCustomerData(int id);
    }

    public class CustomerBusinessLogic
    {
        ICustomerDataAccess _dataAccess;

        public CustomerBusinessLogic(ICustomerDataAccess custDataAccess)
        {
            _dataAccess = custDataAccess;
        }

        public string ProcessCustomerData(int id)
        {
            return _dataAccess.GetCustomerData(id);
        }
    }
    
    public class CustomerService
    {
        CustomerBusinessLogic _customerBL;

        public CustomerService()
        {
            _customerBL = new CustomerBusinessLogic(new CustomerDataAccess());
        }

        public string GetCustomerName(int id)
        {
            return _customerBL.ProcessCustomerData(id);
        }
    }

    public class CustomerService_Constructor_Injection_Data
    {
        CustomerBusinessLogic _customerBL;
        ICustomerDataAccess _dataAccess;

        public CustomerService_Constructor_Injection_Data(ICustomerDataAccess custDataAccess)
        {
            _dataAccess = custDataAccess;
            _customerBL = new CustomerBusinessLogic(_dataAccess);
        }

        public string GetCustomerName(int id)
        {
            return _customerBL.ProcessCustomerData(id);


        }
    }

    public class CustomerDataAccess : ICustomerDataAccess
    {
        public CustomerDataAccess()
        {
        }

        public string GetCustomerData(int id)
        {
            //get the customer name from the db in real application        
            return "Dummy Customer Name DI - Constructor Injection " + id.ToString();
        }
    }

    public class CustomerDataAccess1 : ICustomerDataAccess
    {
        public CustomerDataAccess1()
        {
        }

        public string GetCustomerData(int id)
        {
            //get the customer name from the db in real application        
            return "Dummy Customer Name DI - Constructor Injection (Anden klasse !!!) " + id.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CustomerService CustomerService_Object = new CustomerService();
            Console.WriteLine(CustomerService_Object.GetCustomerName(10));

            CustomerService_Constructor_Injection_Data CustomerService_Object_DI = new CustomerService_Constructor_Injection_Data(new CustomerDataAccess());
            Console.WriteLine(CustomerService_Object_DI.GetCustomerName(12));

            CustomerService_Constructor_Injection_Data CustomerService_Object_DI1 = new CustomerService_Constructor_Injection_Data(new CustomerDataAccess1());
            Console.WriteLine(CustomerService_Object_DI1.GetCustomerName(14));

            CustomerBusinessLogic CustomerBusinessLogic_Object = new CustomerBusinessLogic(new CustomerDataAccess());
            Console.WriteLine(CustomerBusinessLogic_Object.ProcessCustomerData(16));

            CustomerBusinessLogic CustomerBusinessLogic_Object1 = new CustomerBusinessLogic(new CustomerDataAccess1());
            Console.WriteLine(CustomerBusinessLogic_Object1.ProcessCustomerData(18));
            Console.ReadLine();
        }
    }
}
