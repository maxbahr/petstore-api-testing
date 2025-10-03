namespace PetStoreApiTests.Model;

public class ResponseMsgDto
{
    public required int Code { get; set; }
    public required string Type { get; set; }
    public required string Message { get; set; }
}