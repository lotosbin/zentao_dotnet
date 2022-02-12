namespace zentao.client.Core;

public interface IZentaoClient {
    Task<IList<TaskItem>> GetMyTaskAsync(string host, string account, string password);
    Task<List<BugItem>> GetMyBugAsync(string host, string account, string password);
    Task<IList<StoryItem>> GetMyStoryListAsync(string host, string account, string password);
}