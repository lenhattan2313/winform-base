using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDashboard.WPF.Services
{
    public interface IBaseApiService<T> where T : class
    {
        Task<List<T>> GetDataAsync(string line, DateTime from, DateTime to, string search);
    }
}
