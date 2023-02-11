//using g2.Animation.Core.Library.Timing;
//using g2.Animation.Core.ParticleSystems;
//using System.Diagnostics;

//namespace g2.Animation.Tests;

//public class AnimationTests
//{

//    [Fact]
//    public void Animation_ShouldMoveParticleXRightAmount()
//    {

//        // Arrange
//        Particle particle = new(0, 250, 25);
//        double Expected = 1000;
//        Stopwatch sw = Stopwatch.StartNew();

//        // Act
//        long start = sw.ElapsedMilliseconds;
//        long actual = start;
//        Time.Start();
//        while (actual - start <= 1000)
//        {
//            Time.Delta();
//            particle.Move();

//            actual = sw.ElapsedMilliseconds;
//        }

//        Time.Reset();
//        double Actual = particle.X;


//        // Assert
//        Assert.Equal(Expected, Actual);


//        double TotalTicksInMilliseconds()
//        {
//            return sw.ElapsedTicks / Stopwatch.Frequency * 1000.0;
//        }
//    }
//}