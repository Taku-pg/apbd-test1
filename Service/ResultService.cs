namespace apbd_test1.Service;

public class ResultService<T>
{
    public T? Result { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    
    public static ResultService<T> Ok(T result)=>new ResultService<T> { Result = result,Success = true };
    public static ResultService<T> Fail(String msg)=>new ResultService<T> { Message = msg,Success = true };

}