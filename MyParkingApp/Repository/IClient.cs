
using System;
using MyParkingApp.Models;
using MyParkingApp.Models.Dto;

namespace MyParkingApp.Repository
{
	public interface IClient
	{
		Task<IEnumerable<Client>> GetClients();
		Task<Client> GetClient(int id);
		Task<bool> CreateClient(ClientDto clientDto);
		Task<bool> UpdateClient(ClientDto clientDto);
		Task<bool> DeleteClient(int id);
	}
}

