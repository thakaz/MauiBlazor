using MauiBlazor.Shared.Data.Repositories;
using MauiBlazor.Shared.Models;
using MauiBlazor.Shared.Services;
using Microsoft.FluentUI.AspNetCore.Components;
using Moq;

namespace MauiBlazor.Tests;

public class 社員ServiceTest
{
    private readonly Mock<I社員Repository> _社員RepositoryMock;
    private readonly Mock<I社員打刻Repository> _社員打刻RepositoryMock;
    private readonly Mock<I社員カードRepository> _社員カードRepositoryMock;
    private readonly Mock<I通知Service> _通知ServiceMock;
    private readonly 社員Service _社員Service;

    public 社員ServiceTest()
    {
        _社員RepositoryMock = new Mock<I社員Repository>();
        _社員打刻RepositoryMock = new Mock<I社員打刻Repository>();
        _社員カードRepositoryMock = new Mock<I社員カードRepository>();
        _通知ServiceMock = new Mock<I通知Service>();

        _社員Service = new 社員Service(
            _社員RepositoryMock.Object,
            _社員打刻RepositoryMock.Object,
            _社員カードRepositoryMock.Object,
            _通知ServiceMock.Object
        );
    }

    [Fact]
    public async Task カードの登録_有効なIDと社員_カードが登録される()
    {
        // Arrange
        var カードUID = "ABCD1234";
        var 社員ID = 1;
        var 社員番号 = "001";

        var 社員 = new 社員
        {
            Id = 社員ID,
            社員番号 = 社員番号,
            名前性 = "テスト",
            名前名 = "太郎",
            入社年度 = 2025
        };

        // GetByIdAsyncが社員を返すようにモックを設定
        _社員RepositoryMock
            .Setup(repo => repo.GetByIdAsync(社員ID))
            .ReturnsAsync(社員);

        // GetByカードUIDAsyncがnullを返すようにモックを設定（カードが未登録）
        _社員カードRepositoryMock
            .Setup(repo => repo.GetByカードUIDAsync(カードUID))
            .ReturnsAsync((社員カード)null);

        社員カード capturedCard = null;
        _社員カードRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<社員カード>()))
            .Callback<社員カード>(card => capturedCard = card)
            .Returns(Task.FromResult(capturedCard));

        // Act
        await _社員Service.カードの登録(カードUID, 社員ID);

        // Assert
        Assert.NotNull(capturedCard);
        Assert.Equal(社員番号, capturedCard.社員番号);
        Assert.Equal(カードUID, capturedCard.カードUID);
        Assert.Same(社員, capturedCard.社員);

        // 通知が呼び出されたことを検証
        _通知ServiceMock.Verify(
            service => service.ShowToast(
                ToastIntent.Success,
                It.Is<string>(msg => msg.Contains(社員番号) && msg.Contains("カードを追加しました"))
            ),
            Times.Once
        );
    }

    [Fact]
    public async Task カードの登録_存在しない社員ID_例外がスローされる()
    {
        // Arrange
        var カードUID = "ABCD1234";
        var 社員ID = 999; // 存在しないID

        // GetByIdAsyncがnullを返すようにモックを設定
        _社員RepositoryMock
            .Setup(repo => repo.GetByIdAsync(社員ID))
            .ReturnsAsync((社員)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _社員Service.カードの登録(カードUID, 社員ID));
        Assert.Equal("社員が見つかりませんでした", exception.Message);
    }

    [Fact]
    public async Task カードの登録_既に登録済みのカード_例外がスローされる()
    {
        // Arrange
        var カードUID = "ABCD1234";
        var 社員ID = 1;
        var 社員番号 = "001";

        var 社員 = new 社員
        {
            Id = 社員ID,
            社員番号 = 社員番号,
            名前性 = "テスト",
            名前名 = "太郎",
            入社年度 = 2025
        };

        var 既存カード = new 社員カード
        {
            社員番号 = "002", // 別の社員番号
            カードUID = カードUID,
            社員 = new 社員 { Id = 2, 社員番号 = "002", 名前性 = "別の",名前名= "社員", 入社年度 = 2025 }
        };

        // GetByIdAsyncが社員を返すようにモックを設定
        _社員RepositoryMock
            .Setup(repo => repo.GetByIdAsync(社員ID))
            .ReturnsAsync(社員);

        // GetByカードUIDAsyncが既存カードを返すようにモックを設定
        _社員カードRepositoryMock
            .Setup(repo => repo.GetByカードUIDAsync(カードUID))
            .ReturnsAsync(既存カード);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _社員Service.カードの登録(カードUID, 社員ID));
        Assert.Equal("このカードは既に登録されています", exception.Message);

        // 警告通知が呼び出されたことを検証
        _通知ServiceMock.Verify(
            service => service.ShowToast(
                ToastIntent.Warning,
                It.Is<string>(msg => msg.Contains(社員番号) && msg.Contains("このカードは既に登録されています"))
            ),
            Times.Once
        );
    }
}
