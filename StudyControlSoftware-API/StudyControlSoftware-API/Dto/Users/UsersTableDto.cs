namespace StudyControlSoftware_API.Dto.Users
{
    public class UsersTableDto
    {
        public IEnumerable<UserDto>? Users { get; set; }
        public int TotalCount { get; set; }
    }
}
