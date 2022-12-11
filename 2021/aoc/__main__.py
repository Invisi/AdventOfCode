import argparse
import importlib
import time
from io import TextIOWrapper
from pathlib import Path
from typing import List, cast


class AoCMod:
    @staticmethod
    def run(data: List[str]) -> None:
        ...


def import_with_types(name: str) -> AoCMod:
    return cast(AoCMod, importlib.import_module(name))


def day_module(value: str) -> AoCMod:
    try:
        if 1 <= int(value) <= 31:
            # Try to import module
            return import_with_types(f"aoc.days.{value}")
        raise argparse.ArgumentTypeError(f"{value} is not a valid day.")
    except ValueError as e:
        import traceback

        traceback.print_exc()
        raise argparse.ArgumentTypeError(f"{value} is not a valid number.") from e
    except ModuleNotFoundError:
        raise argparse.ArgumentTypeError(
            f"The module for day {value} has not been found."
        )


if __name__ == "__main__":
    parser = argparse.ArgumentParser("aoc")
    parser.add_argument("day", help="The day's implementation to run", type=day_module)
    parser.add_argument(
        "input_file",
        help="The input file",
        type=argparse.FileType("r", encoding="utf8"),
    )

    args = parser.parse_args()

    start = time.perf_counter_ns()
    cast(AoCMod, args.day).run(
        [_.strip() for _ in cast(TextIOWrapper, args.input_file).readlines()]
    )
    stop = time.perf_counter_ns()

    print(f"Implementation ran for {(stop-start)/1000} μs")
