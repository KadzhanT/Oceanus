namespace Oceanus.Domain.Exceptions;
 
 public class VectorServiceException : DomainException
 {
     public VectorServiceException(string message) : base(message) { }
     public VectorServiceException(string message, Exception innerException) 
         : base(message, innerException) { }
 }
