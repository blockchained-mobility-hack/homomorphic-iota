using System;
using Homomorphic_Iota;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace HomomorphicIota.Test
{
    [TestFixture]
    public class UnitTest1
    {
        private Homomorphic _Sut;
        [SetUp]
        public void Setup()
        {
            _Sut = new Homomorphic("");
        }

        [Test]
        public void Position_isIn_ReturnsTrue()
        {
            Position searchPosition = new Position
            {
                Lon = 11.674681,
                Lat = 48.192192
            };

            _Sut.IsIn(searchPosition);
        }
    }
}
