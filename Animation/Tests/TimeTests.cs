//using System.Diagnostics;

//namespace g2.Animation.Tests;
//public class TimeTests
//{
//    [Fact]
//    public void DeltaTime_ShouldReturnTheRightValue()
//    {
//        // Arrange
//        long previousTicks = 0;
//        long actualTicks = 10_000_000;

//        double Expected = 1;

//        // Act
//        double Actual = Delta();

//        // Assert
//        Assert.Equal(Expected, Actual);

//        double Delta()
//        {

//            //DeltaTime = ((watch ??= Stopwatch.StartNew()).Elapsed - previous).TotalSeconds;
//            return (double)(actualTicks - previousTicks) / Stopwatch.Frequency;

//        }
//    }
//}
