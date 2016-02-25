using System.Linq;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class AspectMatchingFamilyTests
    {
        private readonly IAspectMatchingFamily _aspectMatchingFamily;

        public AspectMatchingFamilyTests()
        {
            var channelManager = new ChannelManager();
            _aspectMatchingFamily = new DefaultAspectMatchingFamily<LabelAspect>(channelManager);
        }

        [Fact]
        public void NewEntity_Should_Not_Allow_An_Entity_To_Be_Added_More_Than_Once()
        {
            var e1 = new DefaultEntity("e1", "test").CreateLabelAspect("test", 0, 0);
            Assert.Equal(0, _aspectMatchingFamily.EntireAspectList.Count());
            _aspectMatchingFamily.NewEntity(e1);
            Assert.Equal(1, _aspectMatchingFamily.EntireAspectList.Count());
            _aspectMatchingFamily.NewEntity(e1);
            Assert.Equal(1, _aspectMatchingFamily.EntireAspectList.Count());
        }

        [Fact]
        public void NewEntity_Should_Not_Allow_An_Entity_That_Fails_To_Match_AspectComponents()
        {
            var e1 = new DefaultEntity("e1", "test");
            var e2 = new DefaultEntity("e2", "Test").CreateLabelAspect("Test", 0, 0);
            Assert.Equal(0, _aspectMatchingFamily.EntireAspectList.Count());
            _aspectMatchingFamily.NewEntity(e2);
            Assert.Equal(1, _aspectMatchingFamily.EntireAspectList.Count());
            _aspectMatchingFamily.NewEntity(e1);
            Assert.Equal(1, _aspectMatchingFamily.EntireAspectList.Count());
        }

        [Fact]
        public void RemoveEntity_Should_Delete_Associated_Aspect()
        {
            var deleted = false;
            var e1 = new DefaultEntity("e1", "test").CreateLabelAspect("label", 0, 0);
            _aspectMatchingFamily.NewEntity(e1);
            _aspectMatchingFamily.EntireAspectList.First().Deleted += (s, ea) => deleted = true;
            _aspectMatchingFamily.RemoveEntity(e1);
            Assert.True(deleted);
            Assert.Equal(0, _aspectMatchingFamily.EntireAspectList.Count());
        }

        [Fact]
        public void CleanUp_Should_Delete_All_Aspects()
        {
            var deleteCount = 0;
            var expectedDeleteCount = 3;
            var e1 = new DefaultEntity("e1", "test").CreateLabelAspect("test", 0, 0);
            var e2 = new DefaultEntity("e2", "Test").CreateLabelAspect("test", 0, 0);
            var e3 = new DefaultEntity("e3", "test").CreateLabelAspect("test", 0, 0);
            _aspectMatchingFamily.NewEntity(e1);
            _aspectMatchingFamily.NewEntity(e2);
            _aspectMatchingFamily.NewEntity(e3);
            Assert.Equal(3, _aspectMatchingFamily.EntireAspectList.Count());
            foreach (var aspect in _aspectMatchingFamily.EntireAspectList)
            {
                aspect.Deleted += (s, ea) => deleteCount++;
            }
            _aspectMatchingFamily.CleanUp();
            Assert.Equal(expectedDeleteCount, deleteCount);
            Assert.Equal(0, _aspectMatchingFamily.EntireAspectList.Count());
        }

        [Fact]
        public void Entity_With_Component_Changes_Should_Not_Be_Added_Unless_It_Is_A_Match()
        {
            var e1 = new DefaultEntity("e1", "test").AddComponent(new XYComponent());
            Assert.Equal(0, _aspectMatchingFamily.EntireAspectList.Count());
            _aspectMatchingFamily.ComponentAddedToEntity(e1, typeof (XYComponent));
            Assert.Equal(0, _aspectMatchingFamily.EntireAspectList.Count());
        }

        [Fact]
        public void Entity_With_Component_Changes_Should_Not_Be_Added_If_Component_Is_Wrong_Type()
        {
            var e1 =
                new DefaultEntity("e1", "test").CreateLabelAspect("test", 0, 0).AddComponent(new SomeOtherComponent());
            Assert.Equal(0, _aspectMatchingFamily.EntireAspectList.Count());
            _aspectMatchingFamily.ComponentAddedToEntity(e1, typeof (SomeOtherComponent));
            Assert.Equal(0, _aspectMatchingFamily.EntireAspectList.Count());
        }

        [Fact]
        public void Entity_With_Component_Changes_Should_Be_Added_If_Component_Is_Right_Type_And_Is_Match()
        {
            var e1 = new DefaultEntity("e1", "test").CreateLabelAspect("test", 0, 0);
            Assert.Equal(0, _aspectMatchingFamily.EntireAspectList.Count());
            _aspectMatchingFamily.ComponentAddedToEntity(e1, typeof (XYComponent));
            Assert.Equal(1, _aspectMatchingFamily.EntireAspectList.Count());
        }

        [Fact]
        public void Entity_With_Component_Changes_Should_Not_Be_Removed_If_Family_Does_Not_Contain_Entity()
        {
            var e1 = new DefaultEntity("e1", "test").CreateLabelAspect("test", 0, 0);
            var e2 = new DefaultEntity("e2", "test").CreateLabelAspect("test", 0, 0);
            _aspectMatchingFamily.NewEntity(e1);
            Assert.Equal(1, _aspectMatchingFamily.EntireAspectList.Count());
            _aspectMatchingFamily.ComponentRemovedFromEntity(e2, typeof (XYComponent));
            Assert.Equal(1, _aspectMatchingFamily.EntireAspectList.Count());
        }

        [Fact]
        public void Entity_With_Non_Matching_Component_Type_Should_Not_Trigger_Remove()
        {
            var e1 = new DefaultEntity("e1", "test").CreateLabelAspect("test", 0, 0);
            Assert.Equal(0, _aspectMatchingFamily.EntireAspectList.Count());
            _aspectMatchingFamily.NewEntity(e1);
            Assert.Equal(1, _aspectMatchingFamily.EntireAspectList.Count());
            _aspectMatchingFamily.ComponentRemovedFromEntity(e1, typeof (SomeOtherComponent));
            Assert.Equal(1, _aspectMatchingFamily.EntireAspectList.Count());
        }

        [Fact]
        public void Entity_That_Is_A_Match_And_Has_Matching_Component_Type_Removed_Should_Be_Removed()
        {
            var e1 = new DefaultEntity("e1", "test").CreateLabelAspect("label", 0, 0);
            _aspectMatchingFamily.NewEntity(e1);
            Assert.Equal(1, _aspectMatchingFamily.EntireAspectList.Count());
            _aspectMatchingFamily.ComponentRemovedFromEntity(e1, typeof (XYComponent));
            Assert.Equal(0, _aspectMatchingFamily.EntireAspectList.Count());
        }
    }
}