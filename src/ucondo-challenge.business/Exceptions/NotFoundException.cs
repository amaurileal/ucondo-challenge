namespace ucondo_challenge.business.Exceptions;

public sealed class NotFoundException(string resourceType, string resourceIdentifier) 
    : Exception($"{resourceType} with id: {resourceIdentifier} doesn't exists");