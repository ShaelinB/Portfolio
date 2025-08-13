/* stretch goal - ammo pick up
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnnyRocket
{
    internal class AmmoPickUp : GameObject
    {
        //fields--------------------------

        protected bool isActive;

        //construtor----------------------

        /// <summary>
        /// creates an ammo pick up
        /// </summary>
        /// <param name="pos">the position of the ammo pick up</param>
        /// <param name="texture">how the pick up looks</param>
        public AmmoPickUp(Vector2 pos, Texture2D texture) : base(pos, texture)
        {
            isActive = true;
        }

        //methods--------------------------

        public override bool Collide(GameObject gameObject)
        {
            if (gameObject is AmmoPickUp && isActive) 
            {
                return base.Collide(gameObject);
            }
            else
            {
                return false;
            }

        }
    }
}
*/
