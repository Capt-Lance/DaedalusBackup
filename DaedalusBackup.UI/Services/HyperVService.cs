using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DaedalusBackup.UI.Data;
using Microsoft.AspNetCore.Components;


namespace DaedalusBackup.UI.Services 
{
    public class HyperVService
    {
        private readonly HttpClient _httpClient;

        // public VirtualMachineService(HttpClient httpClient)
        // {
        //     _httpClient = httpClient;
        // }

        public async Task<List<VirtualMachine>> GetVMs()
        {
            Console.WriteLine("Hello world");
            HttpClient httpClient = new HttpClient();
            return await httpClient.GetJsonAsync<List<VirtualMachine>>("http://localhost:4000/api/hyperv/virtualmachines");
            // return await _httpClient.GetJsonAsync<VirtualMachine>("http://localhost:4000/api/hyperv/virtualmachines");
        }
    }
}