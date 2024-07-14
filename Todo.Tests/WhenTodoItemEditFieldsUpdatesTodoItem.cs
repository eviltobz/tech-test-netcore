using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoItems;
using Xunit;

namespace Todo.Tests;

public class WhenTodoItemEditFieldsUpdatesTodoItem
{
    private readonly TodoItem resultItem;
    private readonly TodoItemEditFields editFields;

    public WhenTodoItemEditFieldsUpdatesTodoItem()
    {
        resultItem = new TodoItem(-1, "original responsible party", "original title", Importance.High, 3);
        editFields = new TodoItemEditFields(-1, "list title", -2, "updated title", true, "updated responsible party", Importance.Low, 23);
        TodoItemEditFieldsFactory.Update(editFields, resultItem);
    }

    [Fact]
    public void EqualTodoListId()
    {
        Assert.Equal(editFields.TodoListId, resultItem.TodoListId);
    }

    [Fact]
    public void EqualTitle()
    {
        Assert.Equal(editFields.Title, resultItem.Title);
    }

    [Fact]
    public void EqualImportance()
    {
        Assert.Equal(editFields.Importance, resultItem.Importance);
    }

    [Fact]
    public void EqualRank()
    {
        Assert.Equal(editFields.Rank, resultItem.Rank);
    }
}