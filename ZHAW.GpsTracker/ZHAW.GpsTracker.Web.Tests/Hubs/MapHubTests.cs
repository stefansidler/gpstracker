using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using ZHAW.GpsTracker.Data.Model;
using ZHAW.GpsTracker.Web.Hubs;

namespace ZHAW.GpsTracker.Web.Tests.Hubs
{
    [TestFixture]
    public class MapHubTests
    {
        //public void PropagatePosition_FirstCall_CreatesSessionAndUser()
        //{
        //    var sut = CreateSutWithFakeContext();

        //    Location location = new Location
        //    {
        //        Name = null,
        //        Lat = 123,
        //        Lng = 456,
        //        Speed = 789
        //    };
        //    sut.PropagatePosition(location);


        //}

        //private static Session CreateSession(string url = "http://www.a-url.ch", int id = 1)
        //{
        //    return new Session
        //    {
        //        Id = id,
        //        Url = url,
        //        ProfileAuthenticationProviderDefinition = new Collection<ProfileAuthenticationProviderDefinition>
        //        {
        //            new ProfileAuthenticationProviderDefinition
        //            {
        //                AuthenticationProviderDefinition = new AuthenticationProviderDefinition()
        //            }
        //        }
        //    };
        //}

        //private static TestableMapHub CreateSutWithFakeContext(List<Session> profiles)
        //{
        //    var fakeProfilesSet = CreateFakeSessionSet(profiles);
        //    var sut = new TestableProfileController();
        //    var fakeProfileContext = Substitute.For<ProfileContext>();
        //    fakeProfileContext.Profiles.Returns(fakeProfilesSet);
        //    sut.FakeProfileContext = fakeProfileContext;
        //    return sut;
        //}


        //private static DbSet<Session> CreateFakeSessionSet(List<Session> list)
        //{
        //    IQueryable<Session> sessions = list.AsQueryable();
        //    var fakeSessionSet = Substitute.For<DbSet<Session>, IQueryable<Session>>();
        //    ((IQueryable<Session>)fakeSessionSet).Provider.Returns(sessions.Provider);
        //    ((IQueryable<Session>)fakeSessionSet).Expression.Returns(sessions.Expression);
        //    ((IQueryable<Session>)fakeSessionSet).ElementType.Returns(sessions.ElementType);
        //    ((IQueryable<Session>)fakeSessionSet).GetEnumerator().Returns(sessions.GetEnumerator());
        //    return fakeSessionSet;
        //}
    }
}
