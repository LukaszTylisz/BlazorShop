namespace BlazorShop.Application.Exceptions;

public class NotFoundException(string name, object key)
    : ApplicationException($"Entity {name} with key {key} was not found");