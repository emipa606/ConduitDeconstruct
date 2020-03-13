using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace ConduitDeconstruct
{
    // Token: 0x02000002 RID: 2
    public class Designator_DeconstructPowerConduit : Designator_Deconstruct
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        private bool CheckDef(ThingDef conduit)
        {
            if (conduit == null) return false;
            bool flag = conduit != null;
            string defName = conduit.defName;
            string text = defName;
            if (text != null)
            {
                if (text == "MUR_SubsurfaceConduit")
                {
                    return true;
                }
                if (text == "M13PowerConduit")
                {
                    return true;
                }
                if (text == "M13WaterproofConduit")
                {
                    return true;
                }
            }
            if (conduit.thingClass != typeof(Building)) return false;
            if (conduit.graphicData.linkType != LinkDrawerType.Transmitter) return false;
            if ((conduit.graphicData.linkFlags & LinkFlags.PowerConduit) == LinkFlags.None) return false;
            if (!conduit.placeWorkers.Contains(typeof(PlaceWorker_Conduit))) return false;
            if (conduit.placingDraggableDimensions != 1) return false;
            if (!conduit.EverTransmitsPower) return false;
            if (!conduit.HasComp(typeof(CompPowerTransmitter))) return false;
            if (conduit.altitudeLayer != AltitudeLayer.Conduits) return false;
            return true;
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002138 File Offset: 0x00000338
        public override AcceptanceReport CanDesignateThing(Thing conduit)
        {
            return (base.CanDesignateThing(conduit).Accepted && this.CheckDef(conduit.def)) || (conduit is Blueprint_Build && this.CheckDef(conduit.def.entityDefToBuild as ThingDef));
        }

        // Token: 0x06000003 RID: 3 RVA: 0x00002192 File Offset: 0x00000392
        public override void SelectedUpdate()
        {
            base.SelectedUpdate();
            OverlayDrawHandler.DrawPowerGridOverlayThisFrame();
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000021A2 File Offset: 0x000003A2
        public Designator_DeconstructPowerConduit()
        {
            this.defaultLabel = "Deconstruct Conduits";
            this.icon = ContentFinder<Texture2D>.Get("ToolbarIcon/ConduitDeconstructIcon", true);
            this.hotKey = null;
        }
    }
}
