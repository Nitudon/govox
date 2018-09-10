using UnityEngine;

namespace UdonLib.Commons.Extensions
{
    public static class ColorExtension 
    {
        public static Color ColorAdd(this Color c, Color _c,bool alphaPlus = false)
        {
            if (alphaPlus)
            {
                return new Color(c.r + _c.r, c.g + _c.g, c.b + _c.b, c.a + _c.a);
            }

            return new Color(c.r + _c.r, c.g + _c.g, c.b + _c.b, c.a);
        }

        public static Color AlphaAdd(this Color c, float alpha)
        {
            return new Color(c.r, c.g, c.b, c.a + alpha);
        }

        public static Color AlphaChange(this Color c,float alpha)
        {
            return new Color(c.r,c.g,c.b,alpha);
        }

        public static Color ToVivid(this Color c)
        {
            return new Color(c.r, c.g, c.b, 1);
        }

        public static Color ToClear(this Color c)
        {
            return new Color(c.r,c.g,c.b,0);
        }
    }
}
