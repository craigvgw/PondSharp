using System;
using System.Collections.Generic;
using System.Linq;
using PondSharp.UserScripts;

namespace PondSharp.Client.Pond
{
    public sealed class PondEngine : Engine
    {
        public PondEngine(int width, int height)
        {
            MinX = -width / 2;
            MaxX = width / 2 - 1;
            MinY = -height / 2;
            MaxY = height / 2 - 1;
            ResetSize();
        }

        private Layer<Entity> _entityLayer;
        private List<Entity> _entities = new();
        private readonly Dictionary<int, Entity> _entitiesById = new();
        private readonly List<int> _entitiesToRemoveById = new();
        
        public override IEnumerable<IEntity> Entities => _entities;
        public override IEntity GetEntity(int entityId) => _entitiesById[entityId];
        public override IEntity GetEntityAt(int x, int y) => _entityLayer.GetAt(x, y);
        public int TotalEntities => _entities.Count;
        
        private static int Dist(int x1, int y1, int x2, int y2) =>
            (int) Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        
        private void ResetSize()
        {
            _entityLayer = new(MinX, MinY, MaxX, MaxY);
        }

        private IEnumerable<IEntity> GetEntitiesAround(int centerX, int centerY, int dist)
        {
            return _entityLayer.GetNear(centerX, centerY, dist).Where(entity => Dist(entity.X, entity.Y, centerX, centerY) <= dist);
        }

        public void Tick()
        {
            foreach (var entity in _entities) DoTick(entity);
            foreach (var entityId in _entitiesToRemoveById) DestroyEntityActual(entityId);
            _entitiesToRemoveById.Clear();
        }
        
        public override bool CanMoveTo(IEntity entity, int x, int y)
        {
            return Math.Abs(entity.X - x + entity.Y - y) <= 2 &&
                   x >= MinX && x <= MaxX &&
                   y >= MinY && y <= MaxY && 
                   !(_entityLayer.GetAt(x, y)?.IsBlocking ?? false);
        }

        public override bool CanChangeIsBlocking(IEntity entity) => true;

        public void InsertEntity(Entity entity, int id, int x = 0, int y = 0, int color = 0xFFFFFF, int viewDistance = 0)
        {
            InitializeEntity(entity, new(id)
            {
                X = x, 
                Y = y, 
                Color = color, 
                ViewDistance = viewDistance
            });
            _entitiesById.Add(entity.Id, entity);
            _entityLayer.Add(entity, entity.X, entity.Y);
            _entities.Add(entity);
            OnEntityAdded(entity);
            WasCreated(entity);
        }
        
        public override bool MoveTo(IEntity iEntity, int x, int y)
        {
            if (!(iEntity is Entity entity)) return false;
            if (entity.X == x && entity.Y == y) return true;
            if (!CanMoveTo(entity, x, y)) return false;

            _entityLayer.Move(entity, entity.X, entity.Y, x, y);
            WriteEntityPosition(entity, x, y);
            return true;
        }

        public override IEnumerable<IEntity> GetVisibleEntities(IEntity entity) =>
            GetEntitiesAround(entity.X, entity.Y, entity.ViewDistance).Where(e => e.Id != entity.Id);

        public override bool DestroyEntity(int entityId)
        {
            if (!_entitiesById.ContainsKey(entityId)) return false;
            // This is inefficient if many entities are removed every tick.
            if (_entitiesToRemoveById.Contains(entityId)) return false; 
            _entitiesToRemoveById.Add(entityId);
            return true;
        }

        private void DestroyEntityActual(int entityId)
        {
            var entity = _entitiesById[entityId];
            WasDestroyed(entity);
            OnEntityRemoved(entity);
            _entityLayer.Remove(entity, entity.X, entity.Y);
            _entities.Remove(entity);
            _entitiesById.Remove(entityId);
        }

        public void ClearAllEntities()
        {
            _entities.Clear();
            foreach (var entity in _entitiesById.Values)
            {
                try
                {
                    WasDestroyed(entity);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} {e.StackTrace}");
                }
            }
            _entitiesById.Clear();
            _entityLayer = new(MinX, MinY, MaxX, MaxY);
        }
    }
}