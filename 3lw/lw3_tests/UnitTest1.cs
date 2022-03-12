using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using lw3;

namespace lw3_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateNewTv_WithDefoultTurnedStatus_TurnStatusIsFalse()
        {
            CTVSet tv = new CTVSet();
            Assert.IsFalse(tv.GetTurnStatus());
        }

        [TestMethod]
        public void CreateNewTv_WithDefoultChannel_ChannelIsZero()
        {
            CTVSet tv = new CTVSet();
            Assert.AreEqual(tv.GetChannel(), 0);
        }

        [TestMethod]
        public void CreateNewTv_WithTurnedOn_TurnStatusIsTrue()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            Assert.IsTrue(tv.GetTurnStatus());
        }        
        
        [TestMethod]
        public void CreateNewTv_WithTurnedOn_ChannelIsOne()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            Assert.AreEqual(tv.GetChannel(), 1);
        }

        [TestMethod]
        public void CreateNewTv_WithTurnedOnAndTurnedOff_TurnStatusIsFalse()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            tv.TurnOff();
            Assert.IsFalse(tv.GetTurnStatus());
        }

        [TestMethod]
        public void CreateNewTv_WithTurnedOnAndTurnedOff_ChannelIsZero()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            tv.TurnOff();
            Assert.AreEqual(tv.GetChannel(), 0);
        }
        [TestMethod]
        public void SelectChannel_WithOffTv_ChannelIsZero()
        {
            CTVSet tv = new CTVSet();
            tv.SelectChannel(3);
            Assert.AreEqual(tv.GetChannel(), 0);
        }

        [TestMethod]
        public void SelectChannel_OutOfRangeZero_ChannelIsOne()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            tv.SelectChannel(0);
            Assert.AreEqual(tv.GetChannel(), 1);
        }

        [TestMethod]
        public void SelectChannel_OutOfRangeHundred_ChannelIsOne()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            tv.SelectChannel(100);
            Assert.AreEqual(tv.GetChannel(), 1);
        }

        [TestMethod]
        public void SelectChannel_InRange_ChannelIsSelectedValue()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            tv.SelectChannel(50);
            Assert.AreEqual(tv.GetChannel(), 50);
        }

        [TestMethod]
        public void SelectChannel_BorderValueOne_ChannelIsOne()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            tv.SelectChannel(1);
            Assert.AreEqual(tv.GetChannel(), 1);
        }

        [TestMethod]
        public void SelectChannel_BorderValueNinetyNine_ChannelIsNinetyNine()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            tv.SelectChannel(99);
            Assert.AreEqual(tv.GetChannel(), 99);
        }

        [TestMethod]
        public void SelectChannel_SaveSelectedChannelAfterOnAgain_ChannelIsSelectedChannelBeforeOff()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            tv.SelectChannel(4);
            tv.TurnOff();
            tv.TurnOn();
            Assert.AreEqual(tv.GetChannel(), 4);
        }

        [TestMethod]
        public void TSelectPreviousChannel_WithOffTv_ChannelIsZero()
        {
            CTVSet tv = new CTVSet();
            tv.SelectPreviousChannel();
            Assert.AreEqual(tv.GetChannel(), 0);
        }

        [TestMethod]
        public void SelectPreviousChannel_WithoutSelectedChannel_ChannelIsOne()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            tv.SelectPreviousChannel();
            Assert.AreEqual(tv.GetChannel(), 1);
        }

        [TestMethod]
        public void SelectPreviousChannel_WithSelectedChannelOnce_ChannelIsOne()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            tv.SelectChannel(4);
            tv.SelectPreviousChannel();
            Assert.AreEqual(tv.GetChannel(), 1);
        }
        
        [TestMethod]
        public void SelectPreviousChannel_WithSeveralSelectedChannel_ChannelIsSelectedChannel()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            tv.SelectChannel(4);
            tv.SelectChannel(60);
            tv.SelectPreviousChannel();
            Assert.AreEqual(tv.GetChannel(), 4);
        }

        [TestMethod]
        public void SelectPreviousChannel_SaveSelectedPreviousChannelAfterOnAgain_ChannelIsSelectedPreviousChannelBeforeOff()
        {
            CTVSet tv = new CTVSet();
            tv.TurnOn();
            tv.SelectChannel(4);
            tv.SelectChannel(7);
            tv.TurnOff();
            tv.TurnOn();
            tv.SelectPreviousChannel();
            Assert.AreEqual(tv.GetChannel(), 4);
        }
    }
}
