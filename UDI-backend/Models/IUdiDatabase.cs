using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace UDI_backend.Models {
	public interface IUdiDatabase {
		DbSet<Application> Applications { get; }
		DbSet<Form> Forms { get; }
		DbSet<Reference> References { get; }

		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
	}
}
