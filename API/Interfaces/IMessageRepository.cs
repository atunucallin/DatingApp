using System;
using API.Entities;

namespace API.Interfaces;

public interface IMessageRepository
{

    void AddMessage(Message message);

    void DeleteMessage(Message message);

    Task<Message> GetMessage(int id);

}
