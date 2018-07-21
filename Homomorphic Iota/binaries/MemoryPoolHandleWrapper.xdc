<?xml version="1.0"?><doc>
<members>
<member name="F:seal.util.global_variables.default_coeff_modulus_128" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\util\globals.h" line="24">
This data structure is a key-value storage that maps degrees of the polynomial modulus
to vectors of SmallModulus elements so that when used with the default value for the
standard deviation of the noise distribution (noise_standard_deviation), the security
level is at least 128 bits according to http://HomomorphicEncryption.org. This makes
it easy for non-expert users to select secure parameters.

</member>
<member name="F:seal.util.global_variables.default_coeff_modulus_192" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\util\globals.h" line="33">
This data structure is a key-value storage that maps degrees of the polynomial modulus
to vectors of SmallModulus elements so that when used with the default value for the
standard deviation of the noise distribution (noise_standard_deviation), the security
level is at least 192 bits according to http://HomomorphicEncryption.org. This makes
it easy for non-expert users to select secure parameters.

</member>
<member name="F:seal.util.global_variables.default_coeff_modulus_256" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\util\globals.h" line="42">
This data structure is a key-value storage that maps degrees of the polynomial modulus
to vectors of SmallModulus elements so that when used with the default value for the
standard deviation of the noise distribution (noise_standard_deviation), the security
level is at least 256 bits according to http://HomomorphicEncryption.org. This makes
it easy for non-expert users to select secure parameters.

</member>
<member name="F:seal.util.global_variables.small_mods_60bit" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\util\globals.h" line="51">
            In SEAL the encryption parameter coeff_modulus is a vector of prime numbers
            represented by instances of the SmallModulus class. We present here vectors
            of pre-selected primes that the user can choose from. These are the largest
            60-bit, 50-bit, 40-bit, 30-bit primes that are congruent to 1 modulo 2^18. The
            primes presented here work for poly_modulus up to degree 131072.

            The user can also use their own primes. The only restriction is that they
            must be at most 60 bits in length, and need to be congruent to 1 modulo
            2 * degree(poly_modulus).

</member>
<member name="T:seal.MemoryPoolHandle" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="11">
    Manages a shared pointer to a memory pool. SEAL uses memory pools for 
    improved performance due to the large number of memory allocations needed
    by the homomorphic encryption operations, and the underlying polynomial 
    arithmetic. The library automatically creates a shared global memory pool
    that is used for all dynamic allocations by default, and the user can
    optionally create any number of custom memory pools to be used instead.
    
    @Uses in Multi-Threaded Applications
    Sometimes the user might want to use specific memory pools for dynamic
    allocations in certain functions. For example, in heavily multi-threaded
    applications allocating concurrently from a shared memory pool might lead 
    to significant performance issues due to thread contention. For these cases
    SEAL provides overloads of the functions that take a MemoryPoolHandle as an
    additional argument, and uses the associated memory pool for all dynamic
    allocations inside the function. Whenever this functions is called, the 
    user can then simply pass a thread-local MemoryPoolHandle to be used.
    
    @Thread-Unsafe Memory Pools
    While memory pools are by default thread-safe, in some cases it suffices
    to have a memory pool be thread-unsafe. To get a little extra performance,
    the user can optionally create such thread-unsafe memory pools and use them
    just as they would use thread-safe memory pools.

    @Initialized and Uninitialized Handles
    A MemoryPoolHandle has to be set to point either to the global memory pool,
    or to a new memory pool. If this is not done, the MemoryPoolHandle is
    said to be uninitialized, and cannot be used. Initialization simple means
    assigning MemoryPoolHandle::Global() or MemoryPoolHandle::New() to it.

    @Managing Lifetime
    Internally, the MemoryPoolHandle wraps an std::shared_ptr pointing to
    a SEAL memory pool class. Thus, as long as a MemoryPoolHandle pointing to
    a particular memory pool exists, the pool stays alive. Classes such as
    Evaluator and Ciphertext store their own local copies of a MemoryPoolHandle
    to guarantee that the pool stays alive as long as the managing object 
    itself stays alive. The global memory pool is implemented as a global
    std::shared_ptr to a memory pool class, and is thus expected to stay
    alive for the entire duration of the program execution. Note that it can
    be problematic to create other global objects that use the memory pool
    e.g. in their constructor, as one would have to ensure the initialization
    order of these global variables to be correct (i.e. global memory pool
    first).

</member>
<member name="M:seal.MemoryPoolHandle.#ctor(seal.MemoryPoolHandle!System.Runtime.CompilerServices.IsConst*!System.Runtime.CompilerServices.IsImplicitlyDereferenced)" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="58">
Creates a new uninitialized MemoryPoolHandle.

        Creates a copy of a given MemoryPoolHandle. As a result, the created
        MemoryPoolHandle will point to the same underlying memory pool as the copied
        instance.

        @param[in] copy The MemoryPoolHandle to copy from

</member>
<member name="M:seal.MemoryPoolHandle.#ctor(seal.MemoryPoolHandle*!System.Runtime.CompilerServices.IsImplicitlyDereferenced)" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="75">
        Creates a new MemoryPoolHandle by moving a given one. As a result, the moved
        MemoryPoolHandle will become uninitialized.

        @param[in] source The MemoryPoolHandle to move from

</member>
<member name="M:seal.MemoryPoolHandle.op_Assign(seal.MemoryPoolHandle!System.Runtime.CompilerServices.IsConst*!System.Runtime.CompilerServices.IsImplicitlyDereferenced)" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="86">
        Overwrites the MemoryPoolHandle instance with the specified instance. As a result, 
        the current MemoryPoolHandle will point to the same underlying memory pool as 
        the assigned instance.

        @param[in] assign The MemoryPoolHandle instance to assign to the current instance

</member>
<member name="M:seal.MemoryPoolHandle.op_Assign(seal.MemoryPoolHandle*!System.Runtime.CompilerServices.IsImplicitlyDereferenced)" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="99">
        Moves a specified MemoryPoolHandle instance to the current instance. As a result,
        the assigned MemoryPoolHandle will become uninitialized.

        @param[in] assign The MemoryPoolHandle instance to assign to the current instance

</member>
<member name="M:seal.MemoryPoolHandle.Global" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="111">
Returns a MemoryPoolHandle pointing to the global memory pool.

</member>
<member name="M:seal.MemoryPoolHandle.New" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="139">
Returns a MemoryPoolHandle pointing to a new thread-safe memory pool.

</member>
<member name="M:seal.MemoryPoolHandle.op_Implicit~seal.util.MemoryPool*!System.Runtime.CompilerServices.IsImplicitlyDereferenced" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="147">
        Returns a reference to the internal SEAL memory pool that the MemoryPoolHandle
        points to. This function is mainly for internal use.

        @throws std::logic_error if the MemoryPoolHandle is uninitialized

</member>
<member name="M:seal.MemoryPoolHandle.pool_count" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="164">
        Returns the number of different allocation sizes. This function returns the 
        number of different allocation sizes the memory pool pointed to by the current
        MemoryPoolHandle has made. For example, if the memory pool has only allocated 
        two allocations of sizes 128 KB, this function returns 1. If it has instead
        allocated one allocation of size 64 KB and one of 128 KB, this functions
        returns 2.

        @throws std::logic_error if the MemoryPoolHandle is uninitialized

</member>
<member name="M:seal.MemoryPoolHandle.alloc_uint64_count" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="183">
Returns the size of allocated memory. This functions returns the total amount
of memory (in 64-bit words) allocated by the memory pool pointed to by the 
current MemoryPoolHandle.

@throws std::logic_error if the MemoryPoolHandle is uninitialized

</member>
<member name="M:seal.MemoryPoolHandle.alloc_byte_count" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="199">
        Returns the size of allocated memory. This functions returns the total amount
        of memory (in bytes) allocated by the memory pool pointed to by the current 
        MemoryPoolHandle.

        @throws std::logic_error if the MemoryPoolHandle is uninitialized

</member>
<member name="M:seal.MemoryPoolHandle.op_Implicit~System.Boolean" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="215">
Returns whether the MemoryPoolHandle is initialized.

</member>
<member name="M:seal.MemoryPoolHandle.op_Equality(seal.MemoryPoolHandle!System.Runtime.CompilerServices.IsConst*!System.Runtime.CompilerServices.IsImplicitlyDereferenced)" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="223">
Compares MemoryPoolHandles. This function returns whether the current
MemoryPoolHandle points to the same memory pool as a given MemoryPoolHandle.

</member>
<member name="M:seal.MemoryPoolHandle.op_Inequality(seal.MemoryPoolHandle!System.Runtime.CompilerServices.IsConst*!System.Runtime.CompilerServices.IsImplicitlyDereferenced)" decl="false" source="c:\hackaton\seal_2.3.1\seal\seal\memorypoolhandle.h" line="232">
Compares MemoryPoolHandles. This function returns whether the current
MemoryPoolHandle points to a different memory pool than a given 
MemoryPoolHandle.

</member>
<!-- Discarding badly formed XML document comment for member 'T:Microsoft.Research.SEAL.MemoryPoolHandle'. -->
<member name="M:Microsoft.Research.SEAL.MemoryPoolHandle.#ctor" decl="true" source="c:\hackaton\seal_2.3.1\sealnet\sealnet\memorypoolhandlewrapper.h" line="75">
<summary>Creates a new uninitialized MemoryPoolHandle.</summary>

</member>
<member name="M:Microsoft.Research.SEAL.MemoryPoolHandle.#ctor(Microsoft.Research.SEAL.MemoryPoolHandle)" decl="true" source="c:\hackaton\seal_2.3.1\sealnet\sealnet\memorypoolhandlewrapper.h" line="80">
                <summary>Creates a copy of a given MemoryPoolHandle.</summary>

                <remarks>
                Creates a copy of a given MemoryPoolHandle. As a result, the created
                MemoryPoolHandle will point to the same underlying memory pool as the copied
                instance.
                </remarks>

                <param name="copy">The MemoryPoolHandle to copy from</param>
                <exception cref="T:System.ArgumentNullException">if copy is null</exception>

</member>
<member name="M:Microsoft.Research.SEAL.MemoryPoolHandle.Set(Microsoft.Research.SEAL.MemoryPoolHandle)" decl="true" source="c:\hackaton\seal_2.3.1\sealnet\sealnet\memorypoolhandlewrapper.h" line="94">
                <summary>Overwrites the MemoryPoolHandle instance with the specified 
                instance.</summary>

                <remarks>
                Overwrites the MemoryPoolHandle instance with the specified instance. As a result,
                the current MemoryPoolHandle will point to the same underlying memory pool as the 
                assigned instance.
                </remarks>
                <param name="assign">The MemoryPoolHandle instance to assign to the current 
                instance</param>
                <exception cref="T:System.ArgumentNullException">if assign is null</exception>

</member>
<member name="M:Microsoft.Research.SEAL.MemoryPoolHandle.Global" decl="true" source="c:\hackaton\seal_2.3.1\sealnet\sealnet\memorypoolhandlewrapper.h" line="109">
<summary>Returns a MemoryPoolHandle pointing to the global memory pool.</summary>

</member>
<member name="M:Microsoft.Research.SEAL.MemoryPoolHandle.New" decl="true" source="c:\hackaton\seal_2.3.1\sealnet\sealnet\memorypoolhandlewrapper.h" line="114">
<summary>Returns a MemoryPoolHandle pointing to a new thread-safe memory
pool.</summary>

</member>
<member name="P:Microsoft.Research.SEAL.MemoryPoolHandle.PoolCount" decl="false" source="c:\hackaton\seal_2.3.1\sealnet\sealnet\memorypoolhandlewrapper.h" line="133">
                <summary>Returns the number of different allocation sizes.</summary>

                <remarks>
                Returns the number of different allocation sizes. This function returns the
                number of different allocation sizes the memory pool pointed to by the current
                MemoryPoolHandle has made. For example, if the memory pool has only allocated
                two allocations of sizes 128 KB, this function returns 1. If it has instead
                allocated one allocation of size 64 KB and one of 128 KB, this functions
                returns 2.
                </remarks>
                <exception cref="T:System.InvalidOperationException">if the MemoryPoolHandle is
                uninitialized</exception>

</member>
<member name="P:Microsoft.Research.SEAL.MemoryPoolHandle.AllocUInt64Count" decl="false" source="c:\hackaton\seal_2.3.1\sealnet\sealnet\memorypoolhandlewrapper.h" line="151">
                <summary>Returns the size of allocated memory.</summary>

                <remarks>
                Returns the size of allocated memory. This functions returns the total amount
                of memory (in 64-bit words) allocated by the memory pool pointed to by the
                current MemoryPoolHandle.
                </remarks>
                <exception cref="T:System.InvalidOperationException">if the MemoryPoolHandle is
                uninitialized</exception>

</member>
<member name="P:Microsoft.Research.SEAL.MemoryPoolHandle.AllocByteCount" decl="false" source="c:\hackaton\seal_2.3.1\sealnet\sealnet\memorypoolhandlewrapper.h" line="166">
                <summary>Returns the size of allocated memory.</summary>

                <remarks>
                Returns the size of allocated memory. This functions returns the total amount
                of memory (in bytes) allocated by the memory pool pointed to by the current 
                MemoryPoolHandle.
                </remarks>
                <exception cref="T:System.InvalidOperationException">if the MemoryPoolHandle is
                uninitialized</exception>

</member>
<member name="M:Microsoft.Research.SEAL.MemoryPoolHandle.op_Implicit(Microsoft.Research.SEAL.MemoryPoolHandle)~System.Boolean" decl="true" source="c:\hackaton\seal_2.3.1\sealnet\sealnet\memorypoolhandlewrapper.h" line="181">
<summary>Returns whether the MemoryPoolHandle is initialized.</summary>

</member>
<member name="M:Microsoft.Research.SEAL.MemoryPoolHandle.Dispose" decl="true" source="c:\hackaton\seal_2.3.1\sealnet\sealnet\memorypoolhandlewrapper.h" line="186">
<summary>Destroys the MemoryPoolHandle.</summary>

</member>
<member name="M:Microsoft.Research.SEAL.MemoryPoolHandle.Finalize" decl="true" source="c:\hackaton\seal_2.3.1\sealnet\sealnet\memorypoolhandlewrapper.h" line="191">
<summary>Destroys the MemoryPoolHandle.</summary>

</member>
<member name="M:Microsoft.Research.SEAL.MemoryPoolHandle.GetHandle" decl="true" source="c:\hackaton\seal_2.3.1\sealnet\sealnet\memorypoolhandlewrapper.h" line="199">
<summary>Returns a reference to the underlying C++ MemoryPoolHandle.</summary>

</member>
</members>
</doc>