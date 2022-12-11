from typing import Iterable


def chunk(iter: Iterable, n: int) -> list[list]:
    d = {}
    for i, x in enumerate(iter):
        d.setdefault(i // n, []).append(x)
    return list(d.values())
