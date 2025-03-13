using MauiBlazor.Shared.Models;
using MauiBlazor.Shared.Services;
using MauiBlazor.Shared.Data.Repositories;
using Moq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.FluentUI.AspNetCore.Components;

namespace MauiBlazor.Tests
{

    public class 打刻ServiceTest
    {
        private readonly Mock<I社員Repository> _社員RepositoryMock;
        private readonly Mock<I社員打刻Repository> _社員打刻RepositoryMock;
        private readonly Mock<I通知Service> _通知ServiceMock;
        private readonly 打刻Service _打刻Service;

        public 打刻ServiceTest()
        {
            _社員RepositoryMock = new Mock<I社員Repository>();
            _社員打刻RepositoryMock = new Mock<I社員打刻Repository>();
            _通知ServiceMock = new Mock<I通知Service>();

            _打刻Service = new 打刻Service(
                _社員RepositoryMock.Object,
                _社員打刻RepositoryMock.Object,
                _通知ServiceMock.Object
            );
        }

        [Fact]
        public async Task 打刻_社員番号と時間を指定_打刻データが作成される()
        {
            // Arrange
            var 社員番号 = "001";
            var 打刻時間 = new DateTime(2025, 3, 13, 8, 0, 0);
            var 打刻日 = new DateOnly(2025, 3, 13);

            // 周辺打刻データを空のリストとして返すようにモックを設定
            _社員打刻RepositoryMock
                .Setup(repo => repo.GetBy社員番号Async(社員番号))
                .ReturnsAsync(new List<社員打刻>());

            社員打刻 capturedData = null;
            _社員打刻RepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<社員打刻>()))
                .Callback<社員打刻>(data => capturedData = data)
                .Returns(Task.FromResult(capturedData));

            // Act
            await _打刻Service.打刻(社員番号, 打刻時間);

            // Assert
            Assert.NotNull(capturedData);
            Assert.Equal(社員番号, capturedData.社員番号);
            Assert.Equal(打刻時間, capturedData.打刻時間);
            Assert.Equal(打刻日, capturedData.打刻日);
            Assert.Equal("出勤", capturedData.備考);

            // 通知が呼び出されたことを検証
            _通知ServiceMock.Verify(
                service => service.ShowToast(
                    ToastIntent.Success,
                    It.Is<string>(msg => msg.Contains(社員番号) && msg.Contains("打刻しました"))
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task 打刻ByIDm_有効なIDm_社員が見つかり打刻される()
        {
            // Arrange
            var idm = "ABCD1234";
            var 社員番号 = "001";
            var 打刻時間 = new DateTime(2025, 3, 13, 8, 0, 0);

            var 社員 = new 社員
            {
                Id = 1,
                社員番号 = 社員番号,
                名前 = "テスト太郎",
                入社年度 = 2025
            };

            // GetByカードUIDAsyncが社員を返すようにモックを設定
            _社員RepositoryMock
                .Setup(repo => repo.GetByカードUIDAsync(idm))
                .ReturnsAsync(社員);

            // 周辺打刻データを空のリストとして返すようにモックを設定
            _社員打刻RepositoryMock
                .Setup(repo => repo.GetBy社員番号Async(社員番号))
                .ReturnsAsync(new List<社員打刻>());

            社員打刻 capturedData = null;
            _社員打刻RepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<社員打刻>()))
                .Callback<社員打刻>(data => capturedData = data)
                .Returns(Task.FromResult(capturedData));

            // Act
            await _打刻Service.打刻ByIDm(idm, 打刻時間);

            // Assert
            Assert.NotNull(capturedData);
            Assert.Equal(社員番号, capturedData.社員番号);

            // 通知が呼び出されたことを検証
            _通知ServiceMock.Verify(
                service => service.ShowToast(
                    ToastIntent.Success,
                    It.Is<string>(msg => msg.Contains(社員番号) && msg.Contains("打刻しました"))
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task 打刻ByIDm_無効なIDm_例外がスローされる()
        {
            // Arrange
            var idm = "INVALID";
            var 打刻時間 = new DateTime(2025, 3, 13, 8, 0, 0);

            // GetByカードUIDAsyncがnullを返すようにモックを設定
            _社員RepositoryMock
                .Setup(repo => repo.GetByカードUIDAsync(idm))
                .ReturnsAsync((社員)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _打刻Service.打刻ByIDm(idm, 打刻時間));
            Assert.Equal("社員が見つかりませんでした", exception.Message);
        }
    }
}