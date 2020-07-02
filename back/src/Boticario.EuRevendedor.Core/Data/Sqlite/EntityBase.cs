using Boticario.EuRevendedor.Core.Data.Sqlite.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boticario.EuRevendedor.Core.Data.Sqlite
{
    public class EntityBase : IEntityBase
    {
        #region [ Properties ]

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>
        /// The created time.
        /// </value>
        /// [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the modified time.
        /// </summary>
        /// <value>
        /// The modified time.
        /// </value>
        /// [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ModifiedTime { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion [ Properties ]

        public override string ToString()
        {
            return GetType().Name + $" [Id={Id}] - [CreatedAt={CreatedTime}] - [ModifiedTime={ModifiedTime}]";
        }
    }
}
