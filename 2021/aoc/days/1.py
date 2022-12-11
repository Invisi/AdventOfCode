from typing import List


def run(data: List[str]) -> None:
    data = [int(x) for x in data]
    counter = sum([1 for i in range(len(data[:-1])) if data[i + 1] > data[i]])
    print(f"Part 1: {counter} measurements are larger than the previous measurement.")

    counter = sum(
        [
            1
            for i in range(len(data[:-2]))
            if sum(data[i + 1 : i + 4]) > sum(data[i : i + 3])
        ]
    )
    print(
        f"Part 2: {counter} three-measurements sliding windows are larger than the previous measurement."
    )
