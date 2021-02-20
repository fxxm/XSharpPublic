﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using static System.Diagnostics.Debug;

namespace XSharp.MacroCompiler.Syntax
{
    using static TokenAttr;

    abstract internal partial class Stmt : Node
    {
        internal bool RequireExceptionHandling = false;
    }
    internal partial class StmtBlock : Stmt
    {
        internal override Node Bind(Binder b)
        {
            for(int i  = 0; i < StmtList.Length; i++)
            {
                b.BindStmt(ref StmtList[i]);
            }
            return null;
        }
    }
    internal partial class ExprStmt : Stmt
    {
        internal override Node Bind(Binder b)
        {
            b.Bind(ref Expr);
            return null;
        }
    }
    internal partial class ExprResultStmt : ExprStmt
    {
        internal Node TargetEntity = null;
        internal override Node Bind(Binder b)
        {
            if (Expr != null)
            {
                b.Bind(ref Expr);
                if (Expr.Datatype.NativeType != NativeType.Void)
                {
                    b.Convert(ref Expr, b.ObjectType);
                }
            }
            Symbol = b.ObjectType;
            if (RequireExceptionHandling || !(b.Entity is Codeblock))
            {
                if (b.Entity == null)
                    throw Error(ErrorCode.NoBindTarget);
                TargetEntity = b.Entity;
            }
            return null;
        }
    }
    internal partial class ReturnStmt : ExprResultStmt
    {
    }
    internal partial class DeclStmt : Stmt
    {
    }
    internal partial class VarDecl : Node
    {
    }
    internal partial class ImpliedVarDecl : VarDecl
    {
    }
    internal partial class FieldDeclStmt : Stmt
    {
    }
    internal partial class EmptyStmt : Stmt
    {
        internal override Node Bind(Binder b)
        {
            return null;
        }
    }
    internal partial class WhileStmt : Stmt
    {
        internal override Node Bind(Binder b)
        {
            b.Bind(ref Cond);
            b.Convert(ref Cond, Compilation.Get(NativeType.Boolean));
            b.Bind(ref Stmt);
            return null;
        }
    }
    internal partial class RepeatStmt : WhileStmt
    {
    }
    internal partial class ForStmt : Stmt
    {
    }
    internal partial class ForeachStmt : Stmt
    {
    }
    internal partial class IfStmt : Stmt
    {
        internal override Node Bind(Binder b)
        {
            b.Bind(ref Cond);
            b.Convert(ref Cond, Compilation.Get(NativeType.Boolean));
            b.BindStmt(ref StmtIf);
            b.BindStmt(ref StmtElse);
            return null;
        }
    }
    internal partial class DoCaseStmt : Stmt
    {
        internal override Node Bind(Binder b)
        {
            for(int i = 0; i < Cases.Length; i++)
                b.Bind(ref Cases[i]);
            b.Bind(ref Otherwise);
            return null;
        }
    }
    internal partial class CaseBlock : Node
    {
        internal override Node Bind(Binder b)
        {
            b.Bind(ref Cond);
            b.Convert(ref Cond, Compilation.Get(NativeType.Boolean));
            b.Bind(ref Stmt);
            return null;
        }
    }

    internal partial class SwitchStmt : Stmt
    {
    }
    internal partial class SwitchBlock : Node
    {
    }
    internal partial class SwitchBlockExpr : SwitchBlock
    {
    }
    internal partial class SwitchBlockType : SwitchBlock
    {
    }
    internal partial class ExitStmt : Stmt
    {
    }
    internal partial class LoopStmt : Stmt
    {
    }
    internal partial class BreakStmt : Stmt
    {
    }
    internal partial class ThrowStmt : Stmt
    {
    }
    internal partial class QMarkStmt : Stmt
    {
    }
    internal partial class QQMarkStmt : QMarkStmt
    {
    }
    internal partial class TryStmt : Stmt
    {
    }
    internal partial class CatchBlock : Node
    {
    }
    internal partial class FinallyBlock : Node
    {
    }
    internal partial class SequenceStmt : Stmt
    {
    }

    internal partial class ScopeStmt : Stmt
    {
    }

    internal partial class LockStmt : Stmt
    {
    }

    internal partial class UsingStmt : Stmt
    {
    }
    internal partial class FixedStmt : Stmt
    {
    }
    internal partial class Script : Node
    {
        internal override Node Bind(Binder b)
        {
            b.Entity = this;
            Symbol = b.ObjectType;
            if (Body != null)
            {
                b.BindStmt(ref Body);
            }
            return null;
        }
    }
}
