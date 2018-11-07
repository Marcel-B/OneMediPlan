using System;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace com.b_velop.OneMediPlan.Domain.Stores
{
    public class LocalDataStore
    {
        public async Task<T> LoadFromDevice<T>(string filename)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var destination = Path.Combine(path, filename);
            var file = new FileInfo(destination);
            if (!file.Exists) return default(T);
            using (var sw = file.OpenText())
            {
                var result = await sw.ReadToEndAsync();
                var obj = JsonConvert.DeserializeObject<T>(result);
                return obj;
            }
        }

        public async Task<bool> PersistToDevice<T>(T obj, string filename)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var destination = Path.Combine(path, filename);
            var file = new FileInfo(destination);
            try
            {
                var dst = JsonConvert.SerializeObject(obj);

                using (var sw = file.CreateText())
                {
                    await sw.WriteAsync(dst);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }

        }
    }
}
