namespace Oceanus.Domain.Exceptions;
 
 public class LlmServiceException : DomainException
 {
     public LlmServiceException(string message) : base(message) { }
     public LlmServiceException(string message, Exception innerException) 
         : base(message, innerException) { }
 }