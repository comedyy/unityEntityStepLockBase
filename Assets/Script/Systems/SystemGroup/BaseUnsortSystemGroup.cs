using System.Collections.Generic;
using Unity.Entities;

public partial class BaseUnsortSystemGroup : ComponentSystemGroup
{
    public BaseUnsortSystemGroup()
    {
        EnableSystemSorting = false;
    }
}