using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StaffBase
{
    public abstract void Talk();
    public abstract void Level();
}

public interface IStaffs
{
    void Job();
}

public class IStaffTest : StaffBase, IStaffs
{
    public void Job()
    {
    }

    public override void Level()
    {
        throw new System.NotImplementedException();
    }

    public override void Talk()
    {
        throw new System.NotImplementedException();
    }

}
