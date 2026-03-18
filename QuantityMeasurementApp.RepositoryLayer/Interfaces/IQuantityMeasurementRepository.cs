using QuantityMeasurementApp.Model.Entities;

namespace QuantityMeasurementRepositoryLayer.Interfaces;

public interface IQuantityMeasurementRepository
{
    void Save(QuantityMeasurementEntity entity);
    List<QuantityMeasurementEntity> GetAll();
}