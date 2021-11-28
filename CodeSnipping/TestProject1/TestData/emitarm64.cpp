// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

/*XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
XX                                                                           XX
XX                             emitArm64.cpp                                 XX
XX                                                                           XX
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
*/

#include "jitpch.h"
#ifdef _MSC_VER
#pragma hdrstop
#endif

#if defined(TARGET_ARM64)

/*****************************************************************************/
/*****************************************************************************/

#include "instr.h"
#include "emit.h"
#include "codegen.h"

/* static */ bool emitter::strictArmAsm = true;

/*****************************************************************************/

const instruction emitJumpKindInstructions[] = {
    INS_nop,

#define JMP_SMALL(en, rev, ins) INS_##ins,
#include "emitjmps.h"
};

const emitJumpKind emitReverseJumpKinds[] = {
    EJ_NONE,

#define JMP_SMALL(en, rev, ins) EJ_##rev,
#include "emitjmps.h"
};

/*****************************************************************************
 * Look up the instruction for a jump kind
 */

/*static*/ instruction emitter::emitJumpKindToIns(emitJumpKind jumpKind)
{
    assert((unsigned)jumpKind < ArrLen(emitJumpKindInstructions));
    return emitJumpKindInstructions[jumpKind];
}

/*****************************************************************************
* Look up the jump kind for an instruction. It better be a conditional
* branch instruction with a jump kind!
*/

/*static*/ emitJumpKind emitter::emitInsToJumpKind(instruction ins)
{
    for (unsigned i = 0; i < ArrLen(emitJumpKindInstructions); i++)
    {
        if (ins == emitJumpKindInstructions[i])
        {
            emitJumpKind ret = (emitJumpKind)i;
            assert(EJ_NONE < ret && ret < EJ_COUNT);
            return ret;
        }
    }
    unreached();
}

/*****************************************************************************
 * Reverse the conditional jump
 */

/*static*/ emitJumpKind emitter::emitReverseJumpKind(emitJumpKind jumpKind)
{
    assert(jumpKind < EJ_COUNT);
    return emitReverseJumpKinds[jumpKind];
}