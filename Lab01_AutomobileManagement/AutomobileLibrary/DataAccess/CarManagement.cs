using Microsoft.EntityFrameworkCore;

namespace AutomobileLibrary.DataAccess
{
    public class CarManagement
    {
        // Using Singleton Pattern
        private static CarManagement instance = null;
        private static readonly object instanceLock = new object();
        private CarManagement() { }
        public static CarManagement Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CarManagement();
                    }
                    return instance;
                }
            }
        }

        // ---------------------------------------------
        // Get all cars
        public IEnumerable<Car> GetCarList()
        {
            List<Car> cars;
            try
            {
                var myStockDB = new MyStockContext();
                cars = myStockDB.Cars.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cars;
        }

        // ---------------------------------------------
        // Get car by ID
        public Car GetCarByID(int carID)
        {
            Car car = null;
            try
            {
                var myStockDB = new MyStockContext();
                car = myStockDB.Cars.SingleOrDefault(car => car.CarId == carID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return car;
        }

        // ---------------------------------------------
        // Add new car
        public void AddNew(Car car)
        {
            try
            {
                Car _car = GetCarByID(car.CarId);
                if (_car == null)
                {
                    var myStockDB = new MyStockContext();
                    myStockDB.Cars.Add(car);
                    myStockDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The car already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ---------------------------------------------
        // Update car
        public void Update(Car car)
        {
            try
            {
                Car _car = GetCarByID(car.CarId);
                if (_car != null)
                {
                    var myStockDB = new MyStockContext();
                    myStockDB.Entry<Car>(car).State = EntityState.Modified;
                    myStockDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The car does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ---------------------------------------------
        // Remove car
        public void Remove(Car car)
        {
            try
            {
                Car _car = GetCarByID(car.CarId);
                if (_car != null)
                {
                    var myStockDB = new MyStockContext();
                    myStockDB.Cars.Remove(car);
                    myStockDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The car does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
