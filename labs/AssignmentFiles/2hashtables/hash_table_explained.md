## What is a hash table?

A **hash table** is a way to store and quickly find data using a **key**. It’s like a `Dictionary<TKey, TValue>` in C#.

You give it a key (like a string `"hello"`), and it stores a value (like `42`) in a place that depends on that key.

---

### How does it know where to store things?

It uses a **hash function**:This function turns the key into a number (called a "hash"). For example, `"hello"` might become `34891`.Then we take that number and reduce it to an index in an array, like `34891 % array.Length`.

Important: this index is **not the key**. It’s just a position in an array, and we still store the actual key and value together at that spot.

---

### What's the problem?

Sometimes, **two different keys** get the **same index** (after hashing and modulo). This is called a **collision**.

We need a way to handle collisions so that both values can still be stored and retrieved correctly.

---

### Ways to handle collisions

#### 1. **Separate chaining**

- Think of the hash table as an array.
- Each slot in the array holds a list (e.g., `List<KeyValuePair<string, int>>`).
- If two keys map to the same index, both are added to the list at that position.

#### 2. **Open addressing**

- Instead of using a list, we try to find another empty spot in the array.
- We follow a rule (called **probing**) to look for the next available slot.

---

### Types of open addressing:

#### a) **Linear probing**

- If index `5` is full, try `6`, then `7`, and so on.
- Formula: `index = (original + i) % size` (where `i = 1, 2, 3...`)

#### b) **Quadratic probing**

- Same idea, but jumps increase quadratically: `+1`, `+4`, `+9`, etc.
- Formula: `index = (original + i*i) % size`

---

### Retrieving a value

1. Hash the key and calculate the starting index.
2. Look at that slot:
   - If it’s empty → the key isn’t in the table.
   - If there’s something there:
     - Check if the **stored key matches** your key.
     - If it matches → return the value.
     - If it doesn't match → continue probing using the same rule.

> 🔑 This is why we always store both the key and the value in the table — so we can check if we found the correct key, even if it had to be stored in a later slot due to a collision.

---

### What about deleting?

#### In separate chaining:

- Deletion is simple: locate the correct list at the hashed index and remove the key from the list.
- Since each bucket is independent, removing an item doesn’t affect the rest of the structure.

#### In open addressing:

- You can’t just clear a slot, because that would break the probe chain for other keys.

### Handling deletion in open addressing

When using open addressing, removing a key is not as simple as just clearing a slot. We still need to ensure that other keys that might have collided and been stored further along can still be found. There are two common approaches to solve this, and each comes with trade-offs:

#### Special placeholder (tombstone)

- You mark the slot with a special value (e.g., a `KeyValuePair` with a null key, a boolean flag, or a dedicated enum state).
- During lookups, these placeholders are skipped for matching but treated as occupied to preserve the search chain.
- During insertions, these slots can be reused as if they were empty.

#### Moving items back (gap-filling)

- Instead of marking deleted, you can try to shift later items backward to fill the gap.
- This approach is more complex because you must preserve the probe sequence:
  - Only move an item if its original hash index is before or equal to the deletion point in the current probing path.
  - If not, moving it could break the probe chain and cause lookups for that key to fail.
- Handling this correctly requires careful implementation and may involve performance costs.

Because of these complexities, most implementations prefer using tombstones for simplicity and correctness.

---

Before we wrap up, there's one more question we haven't answered yet:

If probing means we keep moving forward to find an empty slot — what happens when the table starts getting full? Can we always find space, or does that eventually break down?

### Load factor (α) and why it matters

The **load factor**, written as the Greek letter **α (alpha)**, tells us how full the hash table is:

```
α = number of items stored / total number of slots in the table
```

Example: If the table has 10 slots and you’ve inserted 4 items, then α = 0.4.

This number is important because it affects performance and whether inserting new items will work at all — especially for open addressing methods.

#### Separate chaining:

- Each slot holds a list, so it’s okay to have more than one item at the same index.
- You can go above α = 1 (meaning more items than slots).
- The table still works, but long lists slow things down.

#### Linear probing:

- If a slot is full, it checks the next one, then the next, etc.
- As the table fills up (α gets closer to 1), the probing takes longer and forms clusters of filled slots.
- It still works up to about α = 0.7–0.9, but gets slower.
- Once α = 1 (the table is full), insertion fails — there’s nowhere left to go.

#### Quadratic probing:

- Tries slots further apart (e.g., +1, +4, +9, etc.), which reduces clustering.
- But it doesn’t guarantee that it will try every slot in the table.
- That means insertions can fail **even when there are still empty slots**.
- To avoid this, we usually keep α no higher than **0.5**.

The main takeaway: different collision-handling methods tolerate different maximum load factors. Separate chaining is flexible but slower as it grows. Open addressing is fast when the table is mostly empty but needs to stay below certain α values to keep working well.
