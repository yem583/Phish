﻿using System.Threading.Tasks;
using Phish.Domain;
using Phish.ViewModels;

namespace Phish.Desktop.Wpf.Services
{
    public interface IWebApiClientService
    {
        Task<SetListModel> GetRandomSetlistAsync();
    }
}