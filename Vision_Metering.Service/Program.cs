using Newtonsoft.Json;
using System.Text.Json;
using Vision_Metering.Data;
using Vision_Metering.Options;

class Program
{
    private readonly VisionDbContext _context;
    public Program(VisionDbContext context)
    {
        _context = context;
    }
    static void Main(string[] args)
    {
        using (var context = new VisionDbContext())
        {
            var program = new Program(context);
            program.Run();
        }
    }

    public void Run()
    {
        try
        {
            string jsonFilePath = "config.json";
            string jsonContent = File.ReadAllText(jsonFilePath);

            ConfigOptions config = JsonConvert.DeserializeObject<ConfigOptions>(jsonContent);

            string counterName = config.CounterName + DateTime.Now;

            int? counterValue = config.CounterValue;

            var newCounter = new Counter
            {
                CounterName = counterName,
                CounterValue = counterValue,
                IsChecked = config.IsChecked
            };

            _context.Counters.Add(newCounter);
            _context.SaveChanges();

            Console.WriteLine("Новый счетчик добавлен в базу данных:");
            Console.WriteLine($"Имя счетчика: {newCounter.CounterName}, Показания счетчика: {newCounter.CounterValue}, Проверен: {newCounter.IsChecked}");

            var counters = _context.Counters.Take(100).ToList();

            Console.WriteLine("Первые 100 записей из базы данных:");
            foreach (var counter in counters)
            {
                Console.WriteLine($"Имя счетчика: {counter.CounterName}, Показания счетчика: {counter.CounterValue}, Проверен: {counter.IsChecked}");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл config.json не найден.");
        }
        catch (Newtonsoft.Json.JsonException)
        {
            Console.WriteLine("Ошибка при чтении файла config.json. Проверьте его структуру.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }

        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}



