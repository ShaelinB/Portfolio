using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

//creating an enemy class

namespace JohnnyRocket
{
    /// <summary>
    /// enemy class is a child class from Entity
    /// </summary>
    public class Enemy : Entity
    {
        //fields----------------

        
        private Vector2 velocity;
        private double moveTime;
        private double moveTimer;

        //constructor----------------

        /// <summary>
        /// Creates a moving enemy
        /// </summary>
        /// <param name="pos">Starting position of the enemy</param>
        /// <param name="texture">Image associated with the enemy</param>
        /// <param name="shotTexture">Image of the enemy's bullets</param>
        /// <param name="health">Total health the enemy has</param>
        /// <param name="shotTime">Time interval between shots</param>
        /// <param name="shotDamage">Damage dealt to a player by each bullet</param>
        /// <param name="velocity">Speed the enemy moves in the x,y plane</param>
        /// <param name="moveTime">Time before reversing direction</param>
        public Enemy(
            Vector2 pos, Texture2D[] textures, Texture2D shotTexture,
            int health, int shotTime, int shotDamage, Vector2 velocity, double moveTime)
            : base(pos, textures, shotTexture, health,shotTime,shotDamage, 0.5)
        { 
            this.velocity = velocity;
            this.moveTime = moveTime;
            moveTimer = 0;
        }        

        /// <summary>
        /// Creates a stationary enemy
        /// </summary>
        /// <param name="pos">Position of the enemy</param>
        /// <param name="texture">Image associated with the enemy</param>
        /// <param name="shotTexture">Image of the enemy's bullets</param>
        /// <param name="health">Total health the enemy has</param>
        /// <param name="shotTime">Time interval between shots</param>
        /// <param name="shotDamage">Damage dealt to a player by each bullet</param>
        public Enemy(
            Vector2 pos, Texture2D[] textures, Texture2D shotTexture,
            int health, int shotTime, int shotDamage)
            : base(pos, textures, shotTexture, health, shotTime, shotDamage, 0.5)
        {
            this.velocity = Vector2.Zero;
            moveTimer = 0;
        }

        //methods--------------------

        /// <summary>
        /// Move the enemy and reverse direction
        /// </summary>
        /// <param name="gameTime"></param>
        public void Move(GameTime gameTime)
        {
            // Update position
            this.X += velocity.X;
            this.Y += velocity.Y;

            // Reverse movement if needed
            moveTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (moveTimer >= moveTime)
            {
                velocity.X *= -1;
                velocity.Y *= -1;
                moveTimer = 0;
            }

            // Update direction enum
            if (velocity.X > 0)
            {
                lastDirMoved = Direction.Right;
            }
            else if (velocity.X < 0)
            {
                lastDirMoved = Direction.Left;
            }

            if (velocity.Y < 0)
            {
                lastDirMoved = Direction.Up;
            }
            else if (velocity.Y > 0)
            {
                lastDirMoved = Direction.Down;
            }

            if ((velocity.X == 0) && (velocity.Y == 0))
            {
                lastDirMoved = Direction.Down;
            }
        }

        /// <summary>
        /// Shoot projectile on a timer, in the direction the enemy is facing
        /// </summary>
        public void Shoot()
        {
            float ang = 0;
            if (shotTimer % shotTime < 0.0005)
            {
                // Shoot projectile at the correct angle
                switch (lastDirMoved)
                {
                    case Direction.Up:
                        ang = (float)Math.PI * 3 / 2;
                        break;

                    case Direction.Down:
                        ang = (float)Math.PI / 2;
                        break;

                    case Direction.Left:
                        ang = (float)Math.PI;
                        break;

                    case Direction.Right:
                        ang = 0;
                        break;
                }
                base.Shoot(ang, shotTexture, true);
            }
        }

        /// <summary>
        /// Deals damage to a player if it collides with nonfirendly projectiles
        /// </summary>
        /// <param name="player">Player to check collision against</param>
        public bool DoProjectileDamage(Player player)
        {
            foreach (Projectile projectile in projectiles)
            {
                if (projectile.Rectangle.Intersects(player.Rectangle))
                {
                    if(player.TakeDamage(ShotDamage))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// i'll put shoot in here as well as movement
        /// </summary>
        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            shotTimer += gameTime.ElapsedGameTime.TotalSeconds;
            Shoot();
            UpdateProjectiles();
        }

        /// <summary>
        /// Returns a non-constant copy of an enemy. To be used with constant enemies
        /// </summary>
        public Enemy Copy()
        {
            Enemy enemyToReturn = new Enemy(
                this.pos, this.textures, this.shotTexture, this.Health,
                (int)this.shotTime, this.ShotDamage, this.velocity, this.moveTime);

            return enemyToReturn;
        }
    }
}