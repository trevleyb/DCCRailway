using DccRailway.API.Entities;
using DccRailway.API.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test.RepositoryTests;

[TestClass]
public class SerializedRepositoryTests {
    [TestMethod]
    public void LoadSaveLocoRepository() {
        var locoRepo = new SerializedRepository<Loco, Guid>();
        locoRepo.Add(new Loco { Name = "Loco1" });
        locoRepo.Add(new Loco { Name = "Loco2" });
        locoRepo.Add(new Loco { Name = "Loco3" });
        locoRepo.Add(new Loco { Name = "Loco4" });
        Assert.AreEqual(locoRepo.GetAll().Count(), 4);
        locoRepo.Save("loco.xml");

        var loadRepo = new SerializedRepository<Loco, Guid>("loco.xml");
        Assert.AreEqual(loadRepo.GetAll().Count(), 4);
    }
}