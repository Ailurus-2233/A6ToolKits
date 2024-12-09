namespace ToDoList.Models;

public interface ITask
{
    void AddTag(TaskTag tag);

    void RemoveTag(TaskTag tag);

    void AddSubTask(ITask task);

    void RemoveSubTask(ITask task);

    void AddComment(string comment);
}